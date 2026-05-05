using EventsAPI.Application.Reviews.Commands;
using EventsAPI.Application.Reviews.Models;
using EventsAPI.Application.Reviews.Queries;
using EventsAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace EventsAPI.Controllers;

[ApiController]
[Route("api/v1/reviews")]
[Authorize]
[EnableRateLimiting("fixed")]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewCommand command, CancellationToken cancellationToken)
    {
        var review = await _mediator.Send(command, cancellationToken);
        return Ok(ApiResponse<ReviewDto>.Ok(review));
    }

    [HttpGet("by-photographer/{photographerId:guid}")]
    public async Task<IActionResult> GetByPhotographer(
        [FromRoute] Guid photographerId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var reviews = await _mediator.Send(new GetPhotographerReviewsQuery
        {
            PhotographerId = photographerId,
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

        return Ok(ApiResponse<PagedResult<ReviewDto>>.Ok(reviews));
    }
}
