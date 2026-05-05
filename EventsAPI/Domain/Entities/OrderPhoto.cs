namespace EventsAPI.Domain.Entities;

public class OrderPhoto
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public Guid PhotoId { get; set; }
    public Photo Photo { get; set; } = null!;
}
