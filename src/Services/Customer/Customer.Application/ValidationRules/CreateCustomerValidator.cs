using Customer.Domain.DTOs;
using FluentValidation;

namespace Customer.Application.ValidationRules
{
    public class CreateCustomerValidator : AbstractValidator<CustomerCreateDto>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The name is required.")
                .MaximumLength(100)
                .WithMessage("The name is longer than allowed.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("The email is required.")
                .MaximumLength(250)
                .WithMessage("The name is longer than allowed.")
                .Matches(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$")
                .WithMessage("The email format is invalid.");

            RuleFor(x => x.Address.AddressLine)
                .NotEmpty()
                .WithMessage("The address line is required.");

            RuleFor(x => x.Address.City)
                .NotEmpty()
                .WithMessage("The city is required.");

            RuleFor(x => x.Address.Country)
                .NotEmpty()
                .WithMessage("The country is required.");

            RuleFor(x => x.Address.CityCode)
                .GreaterThan(0)
                .WithMessage("The city code must be greater than 0.");
        }
    }
}
