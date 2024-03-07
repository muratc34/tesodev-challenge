using Customer.Domain.DTOs;
using FluentValidation;

namespace Customer.Application.ValidationRules
{
    public class AddressCreateValidator : AbstractValidator<AddressCreateDto>
    {
        public AddressCreateValidator()
        {
            RuleFor(x => x.AddressLine)
                .NotEmpty()
                .WithMessage("The address line is required");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("The city is required");

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("The country is required");

            RuleFor(x => x.CityCode)
                .NotEmpty()
                .WithMessage("The city code is required");
        }
    }
}
