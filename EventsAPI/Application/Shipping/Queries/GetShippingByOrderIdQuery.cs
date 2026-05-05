using EventsAPI.Application.Shipping.Models;
using MediatR;

namespace EventsAPI.Application.Shipping.Queries;

public class GetShippingByOrderIdQuery : IRequest<ShippingDto?>
{
    public Guid OrderId { get; set; }
}
