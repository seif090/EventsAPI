using EventsAPI.Domain.Enums;

namespace EventsAPI.Application.Orders.Models;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid ClientId { get; set; }
    public Guid AlbumTypeId { get; set; }
    public Guid? BoxTypeId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
}
