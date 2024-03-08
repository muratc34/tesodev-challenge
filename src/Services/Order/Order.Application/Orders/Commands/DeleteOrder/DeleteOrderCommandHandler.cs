using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;

        public DeleteOrderCommandHandler(IRepository<Domain.Entities.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == request.OrderId);
            if (order is null)
                return Result<bool>.Failure(ErrorMessages.Order.NotExist, false);

            await _orderRepository.DeleteAsync(order);
            return Result<bool>.Success(true);
        }
    }
}
