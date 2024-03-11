using FluentValidation;

namespace Order.Application.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("The customer id is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("The quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("The price must be greater than 0.");

            RuleFor(x => x.Address);

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

            RuleFor(x => x.Product.Name)
                .NotEmpty()
                .WithMessage("The product name is required.");

            RuleFor(x => x.Product.ImageUrl)
                .NotEmpty()
                .WithMessage("The product image url is required.");
        }
    }
}
