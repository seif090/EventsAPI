namespace EventsAPI.Domain.Entities;

public class Photographer : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string? Bio { get; set; }
    public string? Location { get; set; }
    public bool IsVerified { get; set; }
    public decimal PricePerHour { get; set; }
    public double RatingAverage { get; set; }
    public int RatingCount { get; set; }

    public ICollection<PhotographerPortfolioItem> PortfolioItems { get; set; } = new List<PhotographerPortfolioItem>();
    public ICollection<PhotographerAvailability> AvailabilitySlots { get; set; } = new List<PhotographerAvailability>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
