using FluentValidation.TestHelper;
using Order.Application.Orders.Commands.CreateOrder;
using Order.Domain.DTOs;

namespace Order.UnitTests.ValidatorTests
{
    public class CreateOrderValidatorTests
    {
        private CreateOrderValidator validator;

        public CreateOrderValidatorTests()
        {
            validator = new CreateOrderValidator();
        }
        [Fact]
        public async Task OrderCreateValidator_Should_Return_CustomerId_Validate_Error()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.Empty, 1, 15, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(o => o.CustomerId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task OrderCreateValidator_Should_Return_Quantity_Validate_Error(int quantity)
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), quantity, 15, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(o => o.Quantity);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task OrderCreateValidator_Should_Return_Price_Validate_Error(int price)
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), 2, price, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(o => o.Price);
        }

        [Fact]
        public async Task OrderCreateValidator_Should_Return_AddressLine_Validate_Error()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), 2, 150, Domain.Enumerations.Status.InProgress, new AddressCreateDto("", "İstanbul", "Türkiye", 34), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Address.AddressLine);
        }

        [Fact]
        public async Task OrderCreateValidator_Should_Return_City_Validate_Error()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), 2, 150, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "", "Türkiye", 34), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Address.City);
        }

        [Fact]
        public async Task OrderCreateValidator_Should_Return_Country_Validate_Error()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), 2, 150, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "", 34), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Address.Country);
        }

        [Fact]
        public async Task OrderCreateValidator_Should_Return_CityCode_Validate_Error()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), 2, 150, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 0), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Address.CityCode);
        }

        [Fact]
        public async Task OrderCreateValidator_Should_Return_ProductName_Validate_Error()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), 2, 150, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", ""));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Product.Name);
        }

        [Fact]
        public async Task OrderCreateValidator_Should_Return_ProductImageUrl_Validate_Error()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), 2, 150, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34), new ProductCreateDto("", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Product.ImageUrl);
        }

        [Fact]
        public async Task OrderCreateValidator_Should_Return_Success()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), 2, 160000, Domain.Enumerations.Status.InProgress, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34), new ProductCreateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(c => c.CustomerId);
            result.ShouldNotHaveValidationErrorFor(c => c.Quantity);
            result.ShouldNotHaveValidationErrorFor(c => c.Price);
            result.ShouldNotHaveValidationErrorFor(c => c.Address.AddressLine);
            result.ShouldNotHaveValidationErrorFor(c => c.Address.City);
            result.ShouldNotHaveValidationErrorFor(c => c.Address.Country);
            result.ShouldNotHaveValidationErrorFor(c => c.Address.CityCode);
            result.ShouldNotHaveValidationErrorFor(c => c.Product.Name);
            result.ShouldNotHaveValidationErrorFor(c => c.Product.ImageUrl);
        }
    }
}
