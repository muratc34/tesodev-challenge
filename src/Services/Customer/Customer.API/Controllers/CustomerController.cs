using Customer.Application.Services;
using Customer.Domain.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ValidationException = Shared.Exceptions.ValidationException;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerService _customerService;
        readonly IValidator<CustomerCreateDto> _customerValidator;
        readonly IValidator<AddressCreateDto> _addressValidator;

        public CustomerController(ICustomerService customerService, 
            IValidator<CustomerCreateDto> customerValidator,
            IValidator<AddressCreateDto> addressValidator)
        {
            _customerService = customerService;
            _customerValidator = customerValidator;
            _addressValidator = addressValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDetailDto>>> Get(CancellationToken cancellationToken)
            => Ok(await _customerService.Get(cancellationToken));

        [HttpGet]
        [Route("{customerId}")]
        public async Task<ActionResult<CustomerDetailDto>> Get(Guid customerId)
            => Ok(await _customerService.Get(customerId));

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(CustomerCreateDto customerDto)
        {
            var address = _addressValidator.Validate(customerDto.Address);
            if (!address.IsValid)
                throw new ValidationException(address.Errors);

            var customer = _customerValidator.Validate(customerDto);
            if (!customer.IsValid)
                throw new ValidationException(customer.Errors);

            return Ok(await _customerService.CreateCustomer(customerDto));
        }

        [HttpPut]
        [Route("{customerId}")]
        public async Task<ActionResult<bool>> Update(Guid customerId, CustomerCreateDto customerDto)
        {
            var address = _addressValidator.Validate(customerDto.Address);
            if (!address.IsValid)
                throw new ValidationException(address.Errors);

            var customer = _customerValidator.Validate(customerDto);
            if (!customer.IsValid)
                throw new ValidationException(customer.Errors);

            return Ok(await _customerService.UpdateCustomer(customerId, customerDto));
        }

        [HttpDelete]
        [Route("{customerId}")]
        public async Task<ActionResult<bool>> Delete(Guid customerId)
            => Ok(await _customerService.DeleteCustomer(customerId));

        [HttpGet]
        [Route("validate/{customerId}")]
        public async Task<ActionResult<bool>> Validate(Guid customerId)
            => Ok(await _customerService.Validate(customerId));
    }
}
