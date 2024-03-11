using FluentValidation.TestHelper;
using Order.Application.Orders.Commands.UpdateOrder;
using Order.Domain.DTOs;
using Order.Domain.Enumerations;

namespace Order.UnitTests.ValidatorTests
{
    public class UpdateOrderValidatorTests
    {
        private UpdateOrderValidator validator;

        public UpdateOrderValidatorTests()
        {
            validator = new UpdateOrderValidator();
        }

        [Fact]
        public async Task UpdateOrderValidator_Should_Return_Id_Error()
        {
            //Arrange
            var model = new UpdateOrderCommand(Guid.Empty, 2, 15, Status.InProgress, new AddressUpdateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34), new ProductUpdateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(o => o.Id);
        }

        [Fact]
        public async Task UpdateOrderValidator_Should_Return_Success()
        {
            //Arrange
            var model = new UpdateOrderCommand(Guid.NewGuid(), 2, 160000, Status.InProgress, new AddressUpdateDto("Test Mahallesi No:1 Daire:1", "İstanbul", "Türkiye", 34), new ProductUpdateDto("Uploads/Images/iphone-15.jpeg", "Apple Iphone 15"));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(o => o.Id);
        }

        [Fact]
        public async Task UpdateOrderValidator_Should_Return_Success_With_Null_Datas()
        {
            //Arrange
            var model = new UpdateOrderCommand(Guid.NewGuid(), null, null, null, new AddressUpdateDto(null, null, null, null), new ProductUpdateDto(null, null));
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(o => o.Id);
        }
    }
}
