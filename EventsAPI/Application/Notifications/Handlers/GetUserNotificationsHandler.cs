using EventsAPI.Application.Notifications.Models;
using EventsAPI.Application.Notifications.Queries;
using EventsAPI.Infrastructure.Persistence;
using EventsAPI.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Notifications.Handlers;

public class GetUserNotificationsHandler : IRequestHandler<GetUserNotificationsQuery, PagedResult<NotificationDto>>
{
    private readonly AppDbContext _dbContext;

    public GetUserNotificationsHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<NotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Notifications.Where(n => n.UserId == request.UserId);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderByDescending(notification => notification.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(notification => new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<NotificationDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}
