using AutoMapper;
using Customer.Application.Services;
using Customer.Domain.DTOs;
using Customer.Domain.Mapper;
using Moq;
using Shared.Core.Repositories;

namespace Customer.UnitTests.ServiceTests
{
    public class CustomerServiceTests
    {
        private readonly Mock<IRepository<Domain.Entities.Customer>> mockCustomerRepository;
        private readonly Mock<IRepository<Domain.Entities.Address>> mockAddressRepository;
        private readonly IMapper mapper;
        public CustomerServiceTests()
        {
            mockCustomerRepository = new Mock<IRepository<Domain.Entities.Customer>>();
            mockAddressRepository = new Mock<IRepository<Domain.Entities.Address>>();
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
                cfg.AddProfile(new AddressProfile());
            }).CreateMapper();
        }

        [Fact]
        public async Task CreateCustomer_Should_Return_Success()
        {
            //Arrange
            var customerDto = new CustomerCreateDto("Test", "test@test.com", new AddressCreateDto("Test Mahallesi", "Ýstanbul", "Türkiye", 34));
            var service = new CustomerService(mockCustomerRepository.Object, mockAddressRepository.Object, mapper);
            //Act
            var result = await service.CreateCustomer(customerDto);
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteCustomer_Should_Return_Success()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            var service = new CustomerService(mockCustomerRepository.Object, mockAddressRepository.Object, mapper);
            //Act
            var result = await service.DeleteCustomer(customerId);
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateCustomer_Should_Return_Success()
        {
            //Arrange
            var customerDto = new CustomerUpdateDto(Guid.NewGuid(), "Test", "test@test.com", new AddressUpdateDto("Test Mahallesi", "Ýstanbul", "Türkiye", 34));
            var service = new CustomerService(mockCustomerRepository.Object, mockAddressRepository.Object, mapper);
            //Act
            var result = await service.UpdateCustomer(customerDto);
            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}