﻿using MassTransit;
using Order.Application.Contracts;
using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Order.Domain.Entities;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;
using System.Text.Json;

namespace Order.Application.Orders.Commands.CreateOrder
{
    internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly HttpClient _httpClient;

        public CreateOrderCommandHandler(
            IRepository<Domain.Entities.Order> orderRepository, 
            IRepository<Product> productRepository, 
            IRepository<Address> addressRepository, 
            IHttpClientFactory httpClientFactory,
            IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _addressRepository = addressRepository;
            _httpClient = httpClientFactory.CreateClient();
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customerReponse = await _httpClient.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri($"http://apigateway:80/customer/{request.CustomerId}"),
                Method = HttpMethod.Get,
            });

            if (!customerReponse.IsSuccessStatusCode)
                return Result<Guid>.Failure(ErrorMessages.Order.NotExist, Guid.Empty);

            var str = await customerReponse.Content.ReadAsStringAsync(cancellationToken);
            var json = JsonSerializer.Deserialize<ResponseData>(str);

            if (json.Error is not null)
                return Result<Guid>.Failure(json.Error, Guid.Empty);

            var existAdress = await _addressRepository.GetAsync(x => x.Id == json.Data.Address.Id);
            if (existAdress is null)
            {
                await _addressRepository.CreateAsync(json.Data.Address);
            }

            var product = new Product(request.Product.ImageUrl, request.Product.Name);
            await _productRepository.CreateAsync(product);

            var data = new Domain.Entities.Order(request.CustomerId, request.Quantity, request.Price, request.Status, product.Id, json.Data.Address.Id);
            await _orderRepository.CreateAsync(data);
            await _publishEndpoint.Publish(new Shared.Contracts.AuditLog
            {
                Id = Guid.NewGuid(),
                OrderId = data.Id,
                Action = Shared.Contracts.Action.Create,
                Date = DateTime.UtcNow,
                Message = "The order created."
            });
            return Result<Guid>.Success(data.Id);
        }
    }
}
