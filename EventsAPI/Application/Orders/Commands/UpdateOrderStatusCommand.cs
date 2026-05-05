using EventsAPI.Domain.Enums;
using MediatR;

namespace EventsAPI.Application.Orders.Commands;

public class UpdateOrderStatusCommand : IRequest<bool>
{
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; }
}
