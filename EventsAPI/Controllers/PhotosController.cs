using EventsAPI.Application.Photos.Commands;
using EventsAPI.Application.Photos.Models;
using EventsAPI.Application.Photos.Queries;
using EventsAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace EventsAPI.Controllers;

[ApiController]
[Route("api/v1/photos")]
[Authorize]
[EnableRateLimiting("fixed")]
public class PhotosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PhotosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePhotoCommand command, CancellationToken cancellationToken)
    {
        var photo = await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse<PhotoDto>.Ok(photo));
    }

    [HttpGet("by-booking/{bookingId:guid}")]
    public async Task<IActionResult> GetByBooking([FromRoute] Guid bookingId, CancellationToken cancellationToken)
    {
        var photos = await _mediator.Send(new GetBookingPhotosQuery { BookingId = bookingId }, cancellationToken);
        return Ok(ApiResponse<IReadOnlyCollection<PhotoDto>>.Ok(photos));
    }
}
