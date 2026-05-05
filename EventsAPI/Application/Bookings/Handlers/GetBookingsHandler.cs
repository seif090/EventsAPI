using EventsAPI.Application.Bookings.Models;
using EventsAPI.Application.Bookings.Queries;
using EventsAPI.Application.Common.Interfaces;
using EventsAPI.Infrastructure.Persistence;
using EventsAPI.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Bookings.Handlers;

public class GetBookingsHandler : IRequestHandler<GetBookingsQuery, PagedResult<BookingDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public GetBookingsHandler(AppDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<PagedResult<BookingDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"bookings:{request.ClientId}:{request.PhotographerId}:{request.Page}:{request.PageSize}";
        var cached = await _cacheService.GetAsync<PagedResult<BookingDto>>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

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

        var result = new PagedResult<BookingDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };

        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(2), cancellationToken);
        return result;
    }
}
