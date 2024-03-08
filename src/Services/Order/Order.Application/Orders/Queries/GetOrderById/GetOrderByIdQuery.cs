using Order.Application.Core.Messaging;

namespace Order.Application.Orders.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(Guid OrderId) : IQuery<GetOrderByIdResponse>;
}
