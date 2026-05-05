using EventsAPI.Domain.Enums;
using MediatR;

namespace EventsAPI.Application.Bookings.Commands;

public class UpdateBookingStatusCommand : IRequest<bool>
{
    public Guid BookingId { get; set; }
    public BookingStatus Status { get; set; }
}
