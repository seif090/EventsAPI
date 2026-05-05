using EventsAPI.Application.Bookings.Commands;
using EventsAPI.Application.Bookings.Models;
using EventsAPI.Application.Bookings.Queries;
using EventsAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace EventsAPI.Controllers;

[ApiController]
[Route("api/v1/bookings")]
[Authorize]
[EnableRateLimiting("fixed")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var booking = await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse<BookingDto>.Ok(booking));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var booking = await _mediator.Send(new GetBookingByIdQuery { BookingId = id }, cancellationToken);
        return booking is null
            ? NotFound(ApiResponse<BookingDto>.Fail("NotFound"))
            : Ok(ApiResponse<BookingDto>.Ok(booking));
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] Guid? clientId,
        [FromQuery] Guid? photographerId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetBookingsQuery
        {
            ClientId = clientId,
            PhotographerId = photographerId,
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

        return Ok(ApiResponse<PagedResult<BookingDto>>.Ok(result));
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateBookingStatusCommand command, CancellationToken cancellationToken)
    {
        command.BookingId = id;
        var updated = await _mediator.Send(command, cancellationToken);
        return updated
            ? Ok(ApiResponse<string>.Ok("Updated"))
            : NotFound(ApiResponse<string>.Fail("NotFound"));
    }
}
