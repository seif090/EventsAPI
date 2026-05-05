using EventsAPI.Application.Notifications.Commands;
using EventsAPI.Application.Notifications.Models;
using EventsAPI.Application.Notifications.Queries;
using EventsAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers;

[ApiController]
[Route("api/v1/notifications")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("by-user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(
        [FromRoute] Guid userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var notifications = await _mediator.Send(new GetUserNotificationsQuery
        {
            UserId = userId,
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

        return Ok(ApiResponse<PagedResult<NotificationDto>>.Ok(notifications));
    }

    [HttpPut("{id:guid}/read")]
    public async Task<IActionResult> MarkRead([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new MarkNotificationReadCommand { NotificationId = id }, cancellationToken);
        return updated
            ? Ok(ApiResponse<string>.Ok("Updated"))
            : NotFound(ApiResponse<string>.Fail("NotFound"));
    }
}
