namespace EventsAPI.Domain.Entities;

public class Review : BaseEntity
{
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public Guid PhotographerId { get; set; }
    public Photographer Photographer { get; set; } = null!;

    public Guid ClientId { get; set; }
    public User Client { get; set; } = null!;

    public int Rating { get; set; }
    public string? Comment { get; set; }
}
