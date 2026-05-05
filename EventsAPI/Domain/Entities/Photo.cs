namespace EventsAPI.Domain.Entities;

public class Photo : BaseEntity
{
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public Guid PhotographerId { get; set; }
    public Photographer Photographer { get; set; } = null!;

    public string Url { get; set; } = string.Empty;
    public bool IsSelectedByClient { get; set; }

    public ICollection<OrderPhoto> OrderPhotos { get; set; } = new List<OrderPhoto>();
}
