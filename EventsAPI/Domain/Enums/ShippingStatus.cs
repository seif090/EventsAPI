namespace EventsAPI.Domain.Enums;

public enum ShippingStatus
{
    Pending = 1,
    ReadyForPickup = 2,
    InTransit = 3,
    Delivered = 4,
    Failed = 5
}
