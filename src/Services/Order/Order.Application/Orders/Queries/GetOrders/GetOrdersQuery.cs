using Order.Application.Core.Messaging;

namespace Order.Application.Orders.Queries.GetOrders
{
    public sealed record GetOrdersQuery() : IQuery<List<GetOrdersResponse>>;
}
