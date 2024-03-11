using Customer.Domain.DTOs;
using FluentValidation;

namespace Customer.Application.ValidationRules
{
    public class UpdateCustomerValidator : AbstractValidator<CustomerUpdateDto>
    {
        public UpdateCustomerValidator() 
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("The customerId is required.");
        }
    }
}
