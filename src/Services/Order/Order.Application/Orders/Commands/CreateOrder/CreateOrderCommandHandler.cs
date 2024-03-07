using Order.Application.Core.Messaging;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.CreateOrder
{
    internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;

        public CreateOrderCommandHandler(IRepository<Domain.Entities.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var data = new Domain.Entities.Order(request.CustomerId, request.Quantity, request.Price, request.Status);
            await _orderRepository.CreateAsync(data);
            return Result<Guid>.Success(data.Id);
        }
    }
}
