using Microsoft.EntityFrameworkCore;
using Order.Application.Core.Messaging;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Queries.GetOrdersByCustomerId
{
    internal sealed class GetOrderByCustomerIdQueryHandler
        : IQueryHandler<GetOrderByCustomerIdQuery, List<GetOrderByCustomerIdResponse>>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;

        public GetOrderByCustomerIdQueryHandler(IRepository<Domain.Entities.Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<GetOrderByCustomerIdResponse>>> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken, x => x.Id == request.CustomerId, o => o.Include(x => x.Product));

            var data = new List<GetOrderByCustomerIdResponse>();
            foreach (var order in orders)
            {
                data.Add(new GetOrderByCustomerIdResponse(order.Id, order.CreatedAt, order.UpdatedAt, order.CustomerId, order.Quantity, order.Price, order.Status, order.Product));
            }
            return Result<List<GetOrderByCustomerIdResponse>>.Success(data);
        }
    }
}
