using EventsAPI.Application.Notifications.Commands;
using FluentValidation;

namespace EventsAPI.Application.Notifications.Validators;

public class MarkNotificationReadCommandValidator : AbstractValidator<MarkNotificationReadCommand>
{
    public MarkNotificationReadCommandValidator()
    {
        RuleFor(command => command.NotificationId).NotEmpty();
    }
}
