using EventsAPI.Application.Bookings.Commands;
using FluentValidation;

namespace EventsAPI.Application.Bookings.Validators;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(command => command.ClientId).NotEmpty();
        RuleFor(command => command.PhotographerId).NotEmpty();
        RuleFor(command => command.ScheduledAt).GreaterThan(DateTime.UtcNow.AddMinutes(-1));
        RuleFor(command => command.DurationMinutes).InclusiveBetween(30, 480);
        RuleFor(command => command.Price).GreaterThanOrEqualTo(0);
    }
}
