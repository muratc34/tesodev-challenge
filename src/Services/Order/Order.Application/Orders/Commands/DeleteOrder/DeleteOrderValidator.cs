using FluentValidation;

namespace Order.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("The orderId can not be null.");
        }
    }
}
