using EventsAPI.Application.Bookings.Models;
using EventsAPI.Shared;
using MediatR;

namespace EventsAPI.Application.Bookings.Queries;

public class GetBookingsQuery : IRequest<PagedResult<BookingDto>>
{
    public Guid? ClientId { get; set; }
    public Guid? PhotographerId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
