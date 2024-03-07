using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.UpdateOrder
{
    internal sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;

        public UpdateOrderCommandHandler(IRepository<Domain.Entities.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == request.Id);
            if (order is null)
                return Result<bool>.Failure(ErrorMessages.Order.NotExist, false);

            order.Quantity = request.Quantity;
            order.Price = request.Price;
            order.Status = request.Status;
            order.Products = request.Products;
           
            await _orderRepository.UpdateAsync(order);
            return Result<bool>.Success(true);
        }
    }
}
