using EventsAPI.Application.Orders.Models;
using MediatR;

namespace EventsAPI.Application.Orders.Commands;

public class CreateOrderCommand : IRequest<OrderDto>
{
    public Guid BookingId { get; set; }
    public Guid ClientId { get; set; }
    public Guid AlbumTypeId { get; set; }
    public Guid? BoxTypeId { get; set; }
    public List<Guid> PhotoIds { get; set; } = new();
}
