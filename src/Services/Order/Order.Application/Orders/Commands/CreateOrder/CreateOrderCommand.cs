using Order.Application.Core.Messaging;
using Order.Domain.DTOs;
using Order.Domain.Enumerations;

namespace Order.Application.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(
        Guid CustomerId, 
        int Quantity, 
        double Price, 
        Status Status,
        AddressCreateDto Address,
        ProductCreateDto Product) : ICommand<Guid>;
}
