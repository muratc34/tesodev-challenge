using Moq;
using Order.Application.Clients;
using Order.Application.Orders.Commands.CreateOrder;
using Order.Application.Orders.Commands.DeleteOrder;
using Order.Application.Orders.Commands.UpdateOrder;
using Shared.Core.Repositories;

namespace Order.UnitTests.ServiceTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IRepository<Domain.Entities.Order>> _orderRepositoryMock;
        private readonly Mock<IRepository<Domain.Entities.Address>> _addressRepositoryMock;
        private readonly Mock<IRepository<Domain.Entities.Product>> _productRepositoryMock;
        private readonly Mock<ICustomerClient> _customerClientMock;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IRepository<Domain.Entities.Order>>();
            _addressRepositoryMock = new Mock<IRepository<Domain.Entities.Address>>();
            _productRepositoryMock = new Mock<IRepository<Domain.Entities.Product>>();
            _customerClientMock = new Mock<ICustomerClient>();
        }

        [Fact]
        public async Task CreateOrder_Should_Return_Success()
        {
            //Arrange
            var order = new CreateOrderCommand(Guid.NewGuid(), 1, 15, Domain.Enumerations.Status.InProgress, new Domain.DTOs.AddressCreateDto("Test Mahallesi", "Ýstanbul", "Türkiye", 34), new Domain.DTOs.ProductCreateDto("TestUrl", "Test Product"));
            var service = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _productRepositoryMock.Object, _addressRepositoryMock.Object, null, _customerClientMock.Object);
            //Act
            var result = await service.Handle(order, CancellationToken.None);
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteOrder_Should_Return_Success()
        {
            //Arrange
            var order = new DeleteOrderCommand(Guid.NewGuid());
            var service = new DeleteOrderCommandHandler(_orderRepositoryMock.Object, _productRepositoryMock.Object, _addressRepositoryMock.Object, null);
            //Act
            var result = await service.Handle(order, CancellationToken.None);
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateOrder_Should_Return_Success()
        {
            //Arrange
            var order = new UpdateOrderCommand(Guid.NewGuid(), 1, 15, Domain.Enumerations.Status.InProgress, new Domain.DTOs.AddressUpdateDto("Test Mahallesi", "Ýstanbul", "Türkiye", 34), new Domain.DTOs.ProductUpdateDto("TestUrl", "Test Product"));
            var service = new UpdateOrderCommandHandler(_orderRepositoryMock.Object, _productRepositoryMock.Object, _addressRepositoryMock.Object, null);
            //Act
            var result = await service.Handle(order, CancellationToken.None);
            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}