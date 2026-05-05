using EventsAPI.Domain.Enums;
using MediatR;

namespace EventsAPI.Application.Shipping.Commands;

public class UpdateShippingStatusCommand : IRequest<bool>
{
    public Guid ShippingId { get; set; }
    public ShippingStatus Status { get; set; }
    public string? TrackingNumber { get; set; }
}
