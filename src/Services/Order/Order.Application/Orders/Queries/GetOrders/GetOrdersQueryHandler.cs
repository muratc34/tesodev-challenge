using Microsoft.EntityFrameworkCore;
using Order.Application.Core.Messaging;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Queries.GetOrders
{
    internal sealed class GetOrdersQueryHandler
        : IQueryHandler<GetOrdersQuery, List<GetOrdersResponse>>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;

        public GetOrdersQueryHandler(IRepository<Domain.Entities.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken, null, o => o.Include(x => x.Product));

            var data = new List<GetOrdersResponse>();
            foreach (var order in orders)
            {
                data.Add(new GetOrdersResponse(order.Id, order.CreatedAt, order.UpdatedAt, order.CustomerId, order.Quantity, order.Price, order.Status, order.Product));
            }

            return Result<List<GetOrdersResponse>>.Success(data);
        }
    }
}
