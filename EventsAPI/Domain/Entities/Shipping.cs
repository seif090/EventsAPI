using EventsAPI.Domain.Enums;

namespace EventsAPI.Domain.Entities;

public class Shipping : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string ProviderName { get; set; } = string.Empty;
    public string? TrackingNumber { get; set; }
    public decimal Cost { get; set; }
    public ShippingStatus Status { get; set; } = ShippingStatus.Pending;

    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string Governorate { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}
