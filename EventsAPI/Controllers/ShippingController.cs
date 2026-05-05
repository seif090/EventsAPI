using EventsAPI.Application.Shipping.Commands;
using EventsAPI.Application.Shipping.Models;
using EventsAPI.Application.Shipping.Queries;
using EventsAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace EventsAPI.Controllers;

[ApiController]
[Route("api/v1/shipping")]
[Authorize]
[EnableRateLimiting("fixed")]
public class ShippingController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShippingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShippingCommand command, CancellationToken cancellationToken)
    {
        var shipping = await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse<ShippingDto>.Ok(shipping));
    }

    [HttpGet("by-order/{orderId:guid}")]
    public async Task<IActionResult> GetByOrder([FromRoute] Guid orderId, CancellationToken cancellationToken)
    {
        var shipping = await _mediator.Send(new GetShippingByOrderIdQuery { OrderId = orderId }, cancellationToken);
        return shipping is null
            ? NotFound(ApiResponse<ShippingDto>.Fail("NotFound"))
            : Ok(ApiResponse<ShippingDto>.Ok(shipping));
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateShippingStatusCommand command, CancellationToken cancellationToken)
    {
        command.ShippingId = id;
        var updated = await _mediator.Send(command, cancellationToken);
        return updated
            ? Ok(ApiResponse<string>.Ok("Updated"))
            : NotFound(ApiResponse<string>.Fail("NotFound"));
    }
}
