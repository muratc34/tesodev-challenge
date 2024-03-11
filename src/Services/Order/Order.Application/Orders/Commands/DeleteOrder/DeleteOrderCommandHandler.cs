using MassTransit;
using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Order.Domain.Entities;
using Shared.Contracts;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteOrderCommandHandler(
            IRepository<Domain.Entities.Order> orderRepository, 
            IRepository<Product> productRepository,
            IRepository<Address> addressRepository, 
            IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == request.OrderId);
            if (order is null)
                return Result<bool>.Failure(ErrorMessages.Order.NotExist, false);

            var product = await _productRepository.GetAsync(x => x.Id == order.Product.Id);
            if(product is not null)
                await _productRepository.DeleteAsync(product);

            var address = await _addressRepository.GetAsync(x => x.Id == order.Address.Id);
            if (address is not null)
                await _addressRepository.DeleteAsync(address);

            await _orderRepository.DeleteAsync(order);
            await _publishEndpoint.Publish(new AuditLogCreated
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Action = Shared.Contracts.Action.Delete,
                Date = DateTime.UtcNow,
                Message = "The order deleted."
            });
            return Result<bool>.Success(true);
        }
    }
}
