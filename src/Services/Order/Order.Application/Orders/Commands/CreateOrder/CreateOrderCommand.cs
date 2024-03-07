using Order.Application.Core.Messaging;
using Order.Domain.Entities;
using Order.Domain.Enumerations;

namespace Order.Application.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(
        Guid CustomerId, 
        int Quantity, 
        double Price, 
        Status Status, 
        List<Product>? Products) : ICommand<Guid>;
}
