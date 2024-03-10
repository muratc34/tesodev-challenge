using FluentValidation;

namespace Order.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("The order id is required.");
        }
    }
}
