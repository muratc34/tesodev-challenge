using MassTransit;
using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Order.Domain.Enumerations;
using Shared.Contracts;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.ChangeOrderStatus
{
    internal sealed class ChangeOrderStatusCommandHandler
        : ICommandHandler<ChangeOrderStatusCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ChangeOrderStatusCommandHandler(IRepository<Domain.Entities.Order> orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<bool>> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == request.CustomerId);
            if(order is null)
                return Result<bool>.Failure(ErrorMessages.Order.NotExist, false);

            var isParsed = Enum.TryParse((string)request.Status, true, out Status result);
            if(isParsed is false)
                return Result<bool>.Failure(ErrorMessages.Order.StatusNotExist, false);

            order.Status = result;
            await _orderRepository.UpdateAsync(order);
            await _publishEndpoint.Publish(new AuditLogCreated
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Action = Shared.Contracts.Action.Update,
                Date = DateTime.UtcNow,
                Message = "The order status changed."
            });
            return Result<bool>.Success(true);
        }
    }
}
