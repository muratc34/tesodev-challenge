using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Order.Domain.Entities;
using Shared.Contracts;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateOrderCommandHandler(
            IRepository<Domain.Entities.Order> orderRepository, 
            IRepository<Product> productRepository, 
            IRepository<Address> addressRepository,
            IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _addressRepository = addressRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(o => o.Id == request.Id);
            if (order is null)
                return Result<bool>.Failure(ErrorMessages.Order.NotExist, false);

            if (request.Address is not null)
            {
                var address = await _addressRepository.GetAsync(x => x.Id == order.AddressId);
                if (address is null)
                    return Result<bool>.Failure(ErrorMessages.Address.NotExist, false);

                if(request.Address.AddressLine is not null)
                    address.AddressLine = request.Address.AddressLine;
                if (request.Address.Country is not null)
                    address.Country = request.Address.Country;
                if (request.Address.City is not null)
                    address.City = request.Address.City;
                if (request.Address.CityCode is not null)
                    address.CityCode = (int)request.Address.CityCode;
                await _addressRepository.UpdateAsync(address);
            }

            if (request.Product is not null)
            {
                var product = await _productRepository.GetAsync(x => x.Id == order.ProductId);
                if (product is null)
                    return Result<bool>.Failure(ErrorMessages.Product.NotExist, false);

                if(request.Product.ImageUrl is not null)
                    product.ImageUrl = request.Product.ImageUrl;
                if (request.Product.Name is not null)
                    product.Name = request.Product.Name;
                await _productRepository.UpdateAsync(product);
            }

            if (request.Quantity is not null)
                order.Quantity = (int)request.Quantity;
            if (request.Price is not null)
                order.Price = (double)request.Price;
            if(request.Status is not null)
                order.Status = (Domain.Enumerations.Status)request.Status;

            order.UpdatedAt = DateTime.UtcNow;
            await _orderRepository.UpdateAsync(order);
            await _publishEndpoint.Publish(new AuditLogCreated
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Action = Shared.Contracts.Action.Update,
                Date = DateTime.UtcNow,
                Message = "The order updated."
            });
            return Result<bool>.Success(true);
        }
    }
}
