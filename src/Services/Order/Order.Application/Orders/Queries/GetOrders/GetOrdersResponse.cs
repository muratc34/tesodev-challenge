using Order.Domain.Entities;
using Order.Domain.Enumerations;

namespace Order.Application.Orders.Queries.GetOrders
{
    public sealed record GetOrdersResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid CustomerId,
        int Quantity,
        double Price,
        Status Status,
        List<Product>? Products);
}
