using EventsAPI.Domain.Enums;

namespace EventsAPI.Domain.Entities;

public class Booking : BaseEntity
{
    public Guid ClientId { get; set; }
    public User Client { get; set; } = null!;

    public Guid PhotographerId { get; set; }
    public Photographer Photographer { get; set; } = null!;

    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public string? Notes { get; set; }
    public decimal Price { get; set; }

    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public Order? Order { get; set; }
}
