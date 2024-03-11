using Customer.Application.ValidationRules;
using Customer.Domain.DTOs;
using FluentValidation.TestHelper;

namespace Customer.UnitTests.ValidatorTests
{
    public class UpdateCustomerValidatorTests
    {
        private UpdateCustomerValidator validator;

        public UpdateCustomerValidatorTests()
        {
            validator = new UpdateCustomerValidator();
        }

        [Fact]
        public async Task UpdateCustomerValidator_Should_Return_Id_Error()
        {
            //Arrange
            var model = new CustomerUpdateDto(Guid.Empty, "Test User", "test@test.com", new AddressUpdateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(o => o.CustomerId);
        }

        [Fact]
        public async Task UpdateCustomerValidator_Should_Return_Success()
        {
            //Arrange
            var model = new CustomerUpdateDto(Guid.NewGuid(), "Test User", "test@test.com", new AddressUpdateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(o => o.CustomerId);
        }

        [Fact]
        public async Task UpdateCustomerValidator_Should_Return_Success_With_Null_Datas()
        {
            //Arrange
            var model = new CustomerUpdateDto(Guid.NewGuid(), null, null, new AddressUpdateDto(null, null, null, null));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(o => o.CustomerId);
        }
    }
}
