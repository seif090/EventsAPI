using EventsAPI.Application.Orders.Models;
using MediatR;

namespace EventsAPI.Application.Orders.Queries;

public class GetOrderByIdQuery : IRequest<OrderDto?>
{
    public Guid OrderId { get; set; }
}
