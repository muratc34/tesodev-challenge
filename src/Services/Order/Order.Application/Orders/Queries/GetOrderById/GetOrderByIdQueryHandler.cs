using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Queries.GetOrderById
{
    internal sealed class GetOrderByIdQueryHandler
        : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResponse>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;

        public GetOrderByIdQueryHandler(IRepository<Domain.Entities.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<GetOrderByIdResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == request.OrderId);
            if(order is null)
                return Result<GetOrderByIdResponse>.Failure(ErrorMessages.Order.NotExist, null);

            var response = new GetOrderByIdResponse(order.Id, order.CreatedAt, order.UpdatedAt, order.CustomerId, order.Quantity, order.Price, order.Status, order.Product);
            return Result<GetOrderByIdResponse>.Success(response);
        }
    }
}
