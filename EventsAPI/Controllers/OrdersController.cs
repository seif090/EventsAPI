using EventsAPI.Application.Orders.Commands;
using EventsAPI.Application.Orders.Models;
using EventsAPI.Application.Orders.Queries;
using EventsAPI.Domain.Enums;
using EventsAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace EventsAPI.Controllers;

[ApiController]
[Route("api/v1/orders")]
[Authorize]
[EnableRateLimiting("fixed")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse<OrderDto>.Ok(order));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery { OrderId = id }, cancellationToken);
        return order is null
            ? NotFound(ApiResponse<OrderDto>.Fail("NotFound"))
            : Ok(ApiResponse<OrderDto>.Ok(order));
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] Guid? clientId,
        [FromQuery] OrderStatus? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetOrdersQuery
        {
            ClientId = clientId,
            Status = status,
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

        return Ok(ApiResponse<PagedResult<OrderDto>>.Ok(result));
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        command.OrderId = id;
        var updated = await _mediator.Send(command, cancellationToken);
        return updated
            ? Ok(ApiResponse<string>.Ok("Updated"))
            : NotFound(ApiResponse<string>.Fail("NotFound"));
    }
}
