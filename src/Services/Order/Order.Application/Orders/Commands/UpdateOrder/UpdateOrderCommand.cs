using Order.Application.Core.Messaging;
using Order.Domain.Entities;
using Order.Domain.Enumerations;

namespace Order.Application.Orders.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand(
        Guid Id,
        int Quantity,
        double Price,
        Status Status,
        List<Product>? Products) : ICommand<bool>;
}
