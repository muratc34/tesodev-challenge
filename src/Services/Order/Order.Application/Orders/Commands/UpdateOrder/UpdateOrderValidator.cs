using FluentValidation;

namespace Order.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator() 
        {

        }
    }
}
