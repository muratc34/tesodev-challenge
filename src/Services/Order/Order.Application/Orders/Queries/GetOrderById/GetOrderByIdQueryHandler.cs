using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Application.Core.Errors;
using Order.Application.Core.Messaging;
using Shared.Contracts;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Queries.GetOrderById
{
    internal sealed class GetOrderByIdQueryHandler
        : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResponse>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public GetOrderByIdQueryHandler(IRepository<Domain.Entities.Order> orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<GetOrderByIdResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == request.OrderId, o => o.Include(x => x.Product).Include(x => x.Address));
            if(order is null)
                return Result<GetOrderByIdResponse>.Failure(ErrorMessages.Order.NotExist, null);

            var response = new GetOrderByIdResponse(
                order.Id, 
                order.CreatedAt, 
                order.UpdatedAt, 
                order.CustomerId, 
                order.Quantity, 
                order.Price, 
                order.Status, 
                order.Product,
                order.Address);

            await _publishEndpoint.Publish(new AuditLogCreated
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Action = Shared.Contracts.Action.Get,
                Date = DateTime.UtcNow,
                Message = "The order got by id."
            });
            return Result<GetOrderByIdResponse>.Success(response);
        }
    }
}
