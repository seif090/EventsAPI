using MediatR;

namespace EventsAPI.Application.Notifications.Commands;

public class MarkNotificationReadCommand : IRequest<bool>
{
    public Guid NotificationId { get; set; }
}
