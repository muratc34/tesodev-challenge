using MassTransit;
using Order.Application.Contracts;
using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Shared.Contracts;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteOrderCommandHandler(IRepository<Domain.Entities.Order> orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == request.OrderId);
            if (order is null)
                return Result<bool>.Failure(ErrorMessages.Order.NotExist, false);

            await _orderRepository.DeleteAsync(order);
            await _publishEndpoint.Publish(new AuditLog
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
