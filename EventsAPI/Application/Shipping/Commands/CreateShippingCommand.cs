using EventsAPI.Application.Shipping.Models;
using MediatR;

namespace EventsAPI.Application.Shipping.Commands;

public class CreateShippingCommand : IRequest<ShippingDto>
{
    public Guid OrderId { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string Governorate { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}
