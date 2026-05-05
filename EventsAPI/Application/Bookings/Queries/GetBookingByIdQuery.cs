using EventsAPI.Application.Bookings.Models;
using MediatR;

namespace EventsAPI.Application.Bookings.Queries;

public class GetBookingByIdQuery : IRequest<BookingDto?>
{
    public Guid BookingId { get; set; }
}
