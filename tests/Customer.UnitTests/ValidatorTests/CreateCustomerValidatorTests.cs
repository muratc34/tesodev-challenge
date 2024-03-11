using Customer.Application.ValidationRules;
using Customer.Domain.DTOs;
using FluentValidation.TestHelper;

namespace Customer.UnitTests.ValidatorTests
{
    public class CreateCustomerValidatorTests
    {
        private CreateCustomerValidator validator;

        public CreateCustomerValidatorTests()
        {
            validator = new CreateCustomerValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("testnametestnametestnametestnametestnametestnametestnametestnametestnametestnametestnametestnametestname")]
        public async Task CustomerCreateValidator_Should_Return_Name_Validate_Error(string name)
        {
            //Arrange
            var model = new CustomerCreateDto(name, "test@test.com", new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test@test")]
        [InlineData("test@.com")]
        public async Task CustomerCreateValidator_Should_Return_Email_Validate_Error(string email)
        {
            //Arrange
            var model = new CustomerCreateDto("test", email, new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Fact]
        public async Task CustomerCreateValidator_Should_Return_AddressLine_Validate_Error()
        {
            //Arrange
            var model = new CustomerCreateDto("test", "test@test.com", new AddressCreateDto("", "İstanbul", "Türkiye", 34));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Address.AddressLine);
        }

        [Fact]
        public async Task CustomerCreateValidator_Should_Return_City_Validate_Error()
        {
            //Arrange
            var model = new CustomerCreateDto("test", "test@test.com", new AddressCreateDto("Test Mahallesi No:1 Daire:1", "", "Türkiye", 34));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Address.City);
        }

        [Fact]
        public async Task CustomerCreateValidator_Should_Return_Country_Validate_Error()
        {
            //Arrange
            var model = new CustomerCreateDto("test", "test@test.com", new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "", 34));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Address.Country);
        }

        [Fact]
        public async Task CustomerCreateValidator_Should_Return_CityCode_Validate_Error()
        {
            //Arrange
            var model = new CustomerCreateDto("test", "test@test.com", new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 0));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Address.CityCode);
        }

        [Fact]
        public async Task CustomerCreateValidator_Should_Return_Success()
        {
            //Arrange
            var model = new CustomerCreateDto("test", "test@test.com", new AddressCreateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(c => c.Name);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
            result.ShouldNotHaveValidationErrorFor(c => c.Address.AddressLine);
            result.ShouldNotHaveValidationErrorFor(c => c.Address.City);
            result.ShouldNotHaveValidationErrorFor(c => c.Address.Country);
            result.ShouldNotHaveValidationErrorFor(c => c.Address.CityCode);
        }
    }
}
