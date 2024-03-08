using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Order.Domain.Enumerations;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.ChangeOrderStatus
{
    internal sealed class ChangeOrderStatusCommandHandler
        : ICommandHandler<ChangeOrderStatusCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;

        public ChangeOrderStatusCommandHandler(IRepository<Domain.Entities.Order> orderRepository)
        {
            _orderRepository = orderRepository;
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
            return Result<bool>.Success(true);
        }
    }
}
