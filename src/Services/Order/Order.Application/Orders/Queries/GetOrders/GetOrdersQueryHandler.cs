using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Application.Core.Messaging;
using Shared.Contracts;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Queries.GetOrders
{
    internal sealed class GetOrdersQueryHandler
        : IQueryHandler<GetOrdersQuery, List<GetOrdersResponse>>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public GetOrdersQueryHandler(IRepository<Domain.Entities.Order> orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken, null, o => o.Include(x => x.Product).Include(x => x.Address));

            var data = new List<GetOrdersResponse>();
            foreach (var order in orders)
            {
                data.Add(new GetOrdersResponse(
                    order.Id, 
                    order.CreatedAt, 
                    order.UpdatedAt, 
                    order.CustomerId, 
                    order.Quantity, 
                    order.Price, 
                    order.Status, 
                    order.Product,
                    order.Address));
            }

            await _publishEndpoint.Publish(new AuditLog
            {
                Id = Guid.NewGuid(),
                OrderId = null,
                Action = Shared.Contracts.Action.Get,
                Date = DateTime.UtcNow,
                Message = "The orders listed."
            });
            return Result<List<GetOrdersResponse>>.Success(data);
        }
    }
}
