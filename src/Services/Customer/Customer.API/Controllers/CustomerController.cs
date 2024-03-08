using Customer.Application.Services;
using Customer.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using Shared.Core.Primitives.Result;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<List<CustomerDetailDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
            => Ok(await _customerService.Get(cancellationToken));

        [HttpGet]
        [Route("{customerId}")]
        [ProducesResponseType(typeof(Result<CustomerDetailDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid customerId)
            => Ok(await _customerService.Get(customerId));

        [HttpPost]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CustomerCreateDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _customerService.CreateCustomer(customerDto));
        }

        [HttpPut]
        [Route("{customerId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid customerId, CustomerCreateDto customerDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _customerService.UpdateCustomer(customerId, customerDto));
        }

        [HttpDelete]
        [Route("{customerId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid customerId)
            => Ok(await _customerService.DeleteCustomer(customerId));

        [HttpGet]
        [Route("validate/{customerId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Validate(Guid customerId)
            => Ok(await _customerService.Validate(customerId));
    }
}
