﻿using Customer.Application.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<Domain.Entities.Customer>>> Get(CancellationToken cancellationToken)
            => Ok(await _customerService.Get(cancellationToken));

        [HttpGet]
        [Route("{customerId}")]
        public async Task<ActionResult<Domain.Entities.Customer>> Get(Guid customerId)
            => Ok(await _customerService.Get(customerId));

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(Domain.Entities.Customer customer)
            => Ok(await _customerService.CreateCustomer(customer));

        [HttpPut]
        [Route("{customerId}")]
        public async Task<ActionResult<bool>> Update(Domain.Entities.Customer customer)
            => Ok(await _customerService.UpdateCustomer(customer));

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
