using Order.Domain.Entities;
using Order.Domain.Enumerations;

namespace Order.Application.Orders.Queries.GetOrdersByCustomerId
{
    public sealed record GetOrderByCustomerIdResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid CustomerId,
        int Quantity,
        double Price,
        Status Status,
        List<Product>? Products);
}
