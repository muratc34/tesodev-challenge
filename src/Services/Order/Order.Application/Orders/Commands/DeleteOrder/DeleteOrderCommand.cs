using Order.Application.Core.Messaging;

namespace Order.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderId) : ICommand<bool>;
}
