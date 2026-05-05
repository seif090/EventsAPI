using EventsAPI.Domain.Enums;

namespace EventsAPI.Domain.Entities;

public class Order : BaseEntity
{
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public Guid ClientId { get; set; }
    public User Client { get; set; } = null!;

    public Guid AlbumTypeId { get; set; }
    public AlbumType AlbumType { get; set; } = null!;

    public Guid? BoxTypeId { get; set; }
    public BoxType? BoxType { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalPrice { get; set; }

    public Shipping? Shipping { get; set; }
    public ICollection<OrderPhoto> OrderPhotos { get; set; } = new List<OrderPhoto>();
}
