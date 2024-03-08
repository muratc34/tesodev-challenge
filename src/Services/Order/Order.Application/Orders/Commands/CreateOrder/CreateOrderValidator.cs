using FluentValidation;

namespace Order.Application.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            
        }
    }
}
