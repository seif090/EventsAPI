using EventsAPI.Application.Notifications.Models;
using EventsAPI.Shared;
using MediatR;

namespace EventsAPI.Application.Notifications.Queries;

public class GetUserNotificationsQuery : IRequest<PagedResult<NotificationDto>>
{
    public Guid UserId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
