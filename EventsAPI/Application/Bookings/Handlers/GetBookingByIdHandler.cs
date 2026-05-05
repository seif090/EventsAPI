using EventsAPI.Application.Bookings.Models;
using EventsAPI.Application.Bookings.Queries;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Bookings.Handlers;

public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdQuery, BookingDto?>
{
    private readonly AppDbContext _dbContext;

    public GetBookingByIdHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookingDto?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Bookings
            .Where(booking => booking.Id == request.BookingId)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
