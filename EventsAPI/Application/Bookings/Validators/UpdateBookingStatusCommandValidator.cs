using EventsAPI.Application.Bookings.Commands;
using FluentValidation;

namespace EventsAPI.Application.Bookings.Validators;

public class UpdateBookingStatusCommandValidator : AbstractValidator<UpdateBookingStatusCommand>
{
    public UpdateBookingStatusCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
        RuleFor(command => command.Status).IsInEnum();
    }
}
