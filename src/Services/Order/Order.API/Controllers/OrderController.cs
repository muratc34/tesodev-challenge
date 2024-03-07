using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Orders.Commands.CreateOrder;
using Order.Application.Orders.Commands.DeleteOrder;
using Order.Application.Orders.Commands.UpdateOrder;
using Order.Application.Orders.Queries.GetOrderById;
using Order.Application.Orders.Queries.GetOrders;
using Order.Application.Orders.Queries.GetOrdersByCustomerId;
using Shared.Contracts;
using Shared.Core.Primitives.Result;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISender _sender;

        public OrderController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<Guid>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<List<GetOrdersResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetOrdersQuery(), cancellationToken);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet]
        [Route("{orderId}")]
        [ProducesResponseType(typeof(Result<List<GetOrderByIdResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var query = new GetOrderByIdQuery(orderId);
            var result = await _sender.Send(query);
            return result.IsSuccess ? Ok(result) : NotFound(result.Error);
        }

        [HttpGet]
        [Route("{customerId}")]
        [ProducesResponseType(typeof(Result<List<GetOrderByCustomerIdResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderByCustomerId(Guid customerId)
        {
            var query = new GetOrderByIdQuery(customerId);
            var result = await _sender.Send(query);
            return result.IsSuccess ? Ok(result) : NotFound(result.Error);
        }

        [HttpDelete]
        [Route("{orderId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            var command = new DeleteOrderCommand(orderId);
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPut]
        [Route("{orderId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
