using FluentValidation.TestHelper;
using Order.Application.Orders.Commands.DeleteOrder;

namespace Order.UnitTests.ValidatorTests
{
    public class DeleteOrderValidatorTests
    {
        private DeleteOrderValidator validator;

        public DeleteOrderValidatorTests()
        {
            validator = new DeleteOrderValidator();
        }

        [Fact]
        public async Task DeleteOrderValidator_Should_Return_Id_Error()
        {
            //Arrange
            var model = new DeleteOrderCommand(Guid.Empty);
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldHaveValidationErrorFor(o => o.OrderId);
        }

        [Fact]
        public async Task DeleteOrderValidator_Should_Return_Success()
        {
            //Arrange
            var model = new DeleteOrderCommand(Guid.NewGuid());
            //Act
            var result = await validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(o => o.OrderId);
        }
    }
}
