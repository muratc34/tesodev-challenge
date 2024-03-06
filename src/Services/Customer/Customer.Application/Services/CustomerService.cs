using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;
using Shared.Errors;

namespace Customer.Application.Services
{
    public interface ICustomerService
    {
        Task<Result<Guid>> CreateCustomer(Domain.Entities.Customer customer);
        Task<Result<bool>> DeleteCustomer(Guid customerId);
        Task<Result<bool>> UpdateCustomer(Domain.Entities.Customer customer);
        Task<Result<List<Domain.Entities.Customer>>> Get(CancellationToken cancellationToken);
        Task<Result<Domain.Entities.Customer>> Get(Guid customerId);
        Task<Result<bool>> Validate(Guid customerId);
    }

    public sealed class CustomerService : ICustomerService
    {
        readonly IRepository<Domain.Entities.Customer> _customerRepository;

        public CustomerService(IRepository<Domain.Entities.Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<Guid>> CreateCustomer(Domain.Entities.Customer customer)
        {
            await _customerRepository.CreateAsync(customer);
            return Result<Guid>.Success(customer.Id);
        }

        public async Task<Result<bool>> DeleteCustomer(Guid customerId)
        {
            var existData = await _customerRepository.GetAsync(c => c.Id == customerId);
            if (existData is null)
                return Result<bool>.Failure(ErrorMessages.Customer.NotExist, false);

            await _customerRepository.DeleteAsync(existData);
            return Result<bool>.Success(true);
        }

        public async Task<Result<List<Domain.Entities.Customer>>> Get(CancellationToken cancellationToken)
            => Result<List<Domain.Entities.Customer>>.Success(await _customerRepository.GetAllAsync(cancellationToken));

        public async Task<Result<Domain.Entities.Customer>> Get(Guid customerId)
        {
            var data = await _customerRepository.GetAsync(c => c.Id == customerId);
            if(data is null)
                return Result<Domain.Entities.Customer>.Failure(ErrorMessages.Customer.NotExist, data);
            return Result<Domain.Entities.Customer>.Success(data);
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
