using AutoMapper;
using Customer.Application.Core.Errors;
using Customer.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Customer.Application.Services
{
    public interface ICustomerService
    {
        Task<Result<Guid>> CreateCustomer(CustomerCreateDto customerDto);
        Task<Result<bool>> DeleteCustomer(Guid customerId);
        Task<Result<bool>> UpdateCustomer(Guid id, CustomerCreateDto customerDto);
        Task<Result<List<CustomerDetailDto>>> Get(CancellationToken cancellationToken);
        Task<Result<CustomerDetailDto>> Get(Guid customerId);
        Task<Result<bool>> Validate(Guid customerId);
    }

    public sealed class CustomerService : ICustomerService
    {
        readonly IRepository<Domain.Entities.Customer> _customerRepository;
        readonly IRepository<Domain.Entities.Address> _addressRepository;
        readonly IMapper _mapper;

        public CustomerService(IRepository<Domain.Entities.Customer> customerRepository, IRepository<Domain.Entities.Address> addressRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> CreateCustomer(CustomerCreateDto customerDto)
        {
            var address = _mapper.Map<Domain.Entities.Address>(customerDto.Address);
            await _addressRepository.CreateAsync(address);
            
            var data = _mapper.Map<Domain.Entities.Customer>(customerDto);
            data.AddressId = address.Id;
            await _customerRepository.CreateAsync(data);
            return Result<Guid>.Success(data.Id);
        }

        public async Task<Result<bool>> DeleteCustomer(Guid customerId)
        {
            var customer = await _customerRepository.GetAsync(c => c.Id == customerId);
            if (customer is null)
                return Result<bool>.Failure(ErrorMessages.Customer.NotExist, false);
            await _customerRepository.DeleteAsync(customer);

            var address = await _addressRepository.GetAsync(a => a.Id == customer.AddressId);
            if (address is null)
                return Result<bool>.Failure(ErrorMessages.Customer.NotExist, false);
            await _addressRepository.DeleteAsync(address);
            
            return Result<bool>.Success(true);
        }

        public async Task<Result<List<CustomerDetailDto>>> Get(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken, null, c => c.Include(x => x.Address));
            var data = new List<CustomerDetailDto>();
            foreach(var customer in customers)
            {
                var entity = _mapper.Map<CustomerDetailDto>(customer);
                data.Add(entity);
            }
            return Result<List<CustomerDetailDto>>.Success(data);
        }

        public async Task<Result<CustomerDetailDto>> Get(Guid customerId)
        {
            var data = await _customerRepository.GetAsync(c => c.Id == customerId,  c => c.Include(x => x.Address));
            var mapData = _mapper.Map<CustomerDetailDto>(data);
            if (data is null)
                return Result<CustomerDetailDto>.Failure(ErrorMessages.Customer.NotExist, mapData);
            return Result<CustomerDetailDto>.Success(mapData);
        }

        public async Task<Result<bool>> UpdateCustomer(Guid customerId, CustomerCreateDto customerDto)
        {
            var existData = await _customerRepository.GetAsync(c => c.Id == customerId);
            if (existData is null)
                return Result<bool>.Failure(ErrorMessages.Customer.NotExist, false);

            existData.Email = customerDto.Email;
            existData.Name = customerDto.Name;

            if (existData.Address is not null)
            {
                existData.Address.AddressLine = customerDto.Address.AddressLine;
                existData.Address.City = customerDto.Address.City;
                existData.Address.Country = customerDto.Address.Country;
                existData.Address.CityCode = customerDto.Address.CityCode;
                existData.UpdatedAt = DateTime.UtcNow;
            }

            await _customerRepository.UpdateAsync(existData);

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
