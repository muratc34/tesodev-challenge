using FluentValidation;

namespace Order.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderValidator()
        {

        }
    }
}
