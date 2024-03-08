using Order.Application.Core.Messaging;

namespace Order.Application.Orders.Commands.ChangeOrderStatus
{
    public sealed record ChangeOrderStatusCommand(Guid CustomerId, string Status) : ICommand<bool>;
}
