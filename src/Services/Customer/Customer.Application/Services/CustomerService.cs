using AutoMapper;
using Customer.Domain.DTOs;
using Customer.Domain.Entities;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;
using Shared.Errors;

namespace Customer.Application.Services
{
    public interface ICustomerService
    {
        Task<Result<Guid>> CreateCustomer(CustomerCreateDto customerDto);
        Task<Result<bool>> DeleteCustomer(Guid customerId);
        Task<Result<bool>> UpdateCustomer(Domain.Entities.Customer customer);
        Task<Result<List<CustomerDetailDto>>> Get(CancellationToken cancellationToken);
        Task<Result<CustomerDetailDto>> Get(Guid customerId);
        Task<Result<bool>> Validate(Guid customerId);
    }

    public sealed class CustomerService : ICustomerService
    {
        readonly IRepository<Domain.Entities.Customer> _customerRepository;
        readonly IRepository<Address> _addressRepository;
        readonly IMapper _mapper;

        public CustomerService(IRepository<Domain.Entities.Customer> customerRepository, IRepository<Address> addressRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> CreateCustomer(CustomerCreateDto customerDto)
        {
            var address = _mapper.Map<Address>(customerDto.Address);
            await _addressRepository.CreateAsync(address);
            
            var data = _mapper.Map<Domain.Entities.Customer>(customerDto);
            data.AddressId = address.Id;
            await _customerRepository.CreateAsync(data);
            return Result<Guid>.Success(data.Id);
        }

        public async Task<Result<bool>> DeleteCustomer(Guid customerId)
        {
            var existData = await _customerRepository.GetAsync(c => c.Id == customerId);
            if (existData is null)
                return Result<bool>.Failure(ErrorMessages.Customer.NotExist, false);

            await _customerRepository.DeleteAsync(existData);
            return Result<bool>.Success(true);
        }

        public async Task<Result<List<CustomerDetailDto>>> Get(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);
            var listData = _mapper.Map<List<CustomerDetailDto>>(customers);
            return Result<List<CustomerDetailDto>>.Success(listData);
        }

        public async Task<Result<CustomerDetailDto>> Get(Guid customerId)
        {
            var data = await _customerRepository.GetAsync(c => c.Id == customerId);
            var mapData = _mapper.Map<CustomerDetailDto>(data);
            if(data is null)
                return Result<CustomerDetailDto>.Failure(ErrorMessages.Customer.NotExist, mapData);
            return Result<CustomerDetailDto>.Success(mapData);
        }

        public async Task<Result<bool>> UpdateCustomer(Domain.Entities.Customer customer)
        {
            var existData = await _customerRepository.GetAsync(c => c.Id == customer.Id);
            if (existData is null)
                return Result<bool>.Failure(ErrorMessages.Customer.NotExist, false);
            await _customerRepository.UpdateAsync(customer);
            return Result<bool>.Success(true);
        }
         
        public async Task<Result<bool>> Validate(Guid customerId)
        {
            var existData = await _customerRepository.GetAsync(c => c.Id == customerId);
            if (existData is null)
                return Result<bool>.Failure(ErrorMessages.Customer.NotExist, false);
            return Result<bool>.Success(true);
        }
    }
}
