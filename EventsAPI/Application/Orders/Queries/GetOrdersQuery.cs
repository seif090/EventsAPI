using EventsAPI.Application.Orders.Models;
using EventsAPI.Domain.Enums;
using EventsAPI.Shared;
using MediatR;

namespace EventsAPI.Application.Orders.Queries;

public class GetOrdersQuery : IRequest<PagedResult<OrderDto>>
{
    public Guid? ClientId { get; set; }
    public OrderStatus? Status { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
