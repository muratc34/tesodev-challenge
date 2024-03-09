using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Application.Core.Messaging;
using Shared.Contracts;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Queries.GetOrdersByCustomerId
{
    internal sealed class GetOrderByCustomerIdQueryHandler
        : IQueryHandler<GetOrderByCustomerIdQuery, List<GetOrderByCustomerIdResponse>>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public GetOrderByCustomerIdQueryHandler(IRepository<Domain.Entities.Order> orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<List<GetOrderByCustomerIdResponse>>> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken, x => x.Id == request.CustomerId, o => o.Include(x => x.Product).Include(x => x.Address));

            var data = new List<GetOrderByCustomerIdResponse>();
            foreach (var order in orders)
            {
                data.Add(new GetOrderByCustomerIdResponse(
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

            await _publishEndpoint.Publish(new AuditLogCreated
            {
                Id = Guid.NewGuid(),
                OrderId = null,
                Action = Shared.Contracts.Action.Get,
                Date = DateTime.UtcNow,
                Message = "The orders listed by customer id."
            });
            return Result<List<GetOrderByCustomerIdResponse>>.Success(data);
        }
    }
}
