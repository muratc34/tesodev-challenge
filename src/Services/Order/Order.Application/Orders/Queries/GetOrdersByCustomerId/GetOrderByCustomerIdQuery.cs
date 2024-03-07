using Order.Application.Core.Messaging;

namespace Order.Application.Orders.Queries.GetOrdersByCustomerId
{
    public sealed record GetOrderByCustomerIdQuery(Guid CustomerId) : IQuery<List<GetOrderByCustomerIdResponse>>;
}
