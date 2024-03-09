using MassTransit;
using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Order.Domain.Entities;
using Shared.Contracts;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.UpdateOrder
{
    internal sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, bool>
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
            var order = await _orderRepository.GetAsync(x => x.Id == request.Id);
            if (order is null)
                return Result<bool>.Failure(ErrorMessages.Order.NotExist, false);

            if (request.Address is not null)
            {
                var address = await _addressRepository.GetAsync(x => x.Id == request.Address.AddressId);
                if (address is null)
                    return Result<bool>.Failure(ErrorMessages.Address.NotExist, false);

                address.AddressLine = request.Address.AddressLine;
                address.Country = request.Address.Country;
                address.City = request.Address.City;
                address.CityCode = request.Address.CityCode;
                await _addressRepository.UpdateAsync(address);

                order.AddressId = address.Id;
            }

            if (request.Product is not null)
            {
                var product = await _productRepository.GetAsync(x => x.Id == request.Product.ProductId);
                if (product is null)
                    return Result<bool>.Failure(ErrorMessages.Product.NotExist, false);

                product.ImageUrl = request.Product.ImageUrl;
                product.Name = request.Product.Name;
                await _productRepository.UpdateAsync(product);

                order.ProductId = product.Id;
            }

            order.Quantity = request.Quantity;
            order.Price = request.Price;
            order.Status = request.Status;

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
