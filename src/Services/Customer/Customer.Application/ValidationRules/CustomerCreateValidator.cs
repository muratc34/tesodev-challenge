using Customer.Domain.DTOs;
using FluentValidation;

namespace Customer.Application.ValidationRules
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The name is required.")
                .MaximumLength(100)
                .WithMessage("The name is longer than allowed.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("The email is required")
                .MaximumLength(250)
                .WithMessage("The name is longer than allowed.")
                .Matches(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$")
                .WithMessage("The email format is invalid.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("The address id is required.");
        }
    }
}
