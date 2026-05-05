using EventsAPI.Application.Bookings.Models;
using EventsAPI.Application.Bookings.Queries;
using EventsAPI.Infrastructure.Persistence;
using EventsAPI.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Bookings.Handlers;

public class GetBookingsHandler : IRequestHandler<GetBookingsQuery, PagedResult<BookingDto>>
{
    private readonly AppDbContext _dbContext;

    public GetBookingsHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<BookingDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Bookings.AsQueryable();

        if (request.ClientId.HasValue)
        {
            query = query.Where(booking => booking.ClientId == request.ClientId.Value);
        }

        if (request.PhotographerId.HasValue)
        {
            query = query.Where(booking => booking.PhotographerId == request.PhotographerId.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderByDescending(booking => booking.ScheduledAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(booking => new BookingDto
            {
                Id = booking.Id,
                ClientId = booking.ClientId,
                PhotographerId = booking.PhotographerId,
                ScheduledAt = booking.ScheduledAt,
                DurationMinutes = booking.DurationMinutes,
                Status = booking.Status,
                Notes = booking.Notes,
                Price = booking.Price
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<BookingDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}
