using EventsAPI.Application.Notifications.Commands;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Notifications.Handlers;

public class MarkNotificationReadHandler : IRequestHandler<MarkNotificationReadCommand, bool>
{
    private readonly AppDbContext _dbContext;

    public MarkNotificationReadHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _dbContext.Notifications
            .FirstOrDefaultAsync(n => n.Id == request.NotificationId, cancellationToken);

        if (notification is null)
        {
            return false;
        }

        notification.IsRead = true;
        notification.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
