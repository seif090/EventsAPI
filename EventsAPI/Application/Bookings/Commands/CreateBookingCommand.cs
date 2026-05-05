using EventsAPI.Application.Bookings.Models;
using MediatR;

namespace EventsAPI.Application.Bookings.Commands;

public class CreateBookingCommand : IRequest<BookingDto>
{
    public Guid ClientId { get; set; }
    public Guid PhotographerId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public string? Notes { get; set; }
    public decimal Price { get; set; }
}
