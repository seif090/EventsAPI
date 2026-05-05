using EventsAPI.Domain.Enums;

namespace EventsAPI.Application.Bookings.Models;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid PhotographerId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public BookingStatus Status { get; set; }
    public string? Notes { get; set; }
    public decimal Price { get; set; }
}
