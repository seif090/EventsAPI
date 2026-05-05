using EventsAPI.Application.Bookings.Commands;
using EventsAPI.Application.Bookings.Models;
using EventsAPI.Domain.Entities;
using EventsAPI.Domain.Enums;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Bookings.Handlers;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingDto>
{
    private readonly AppDbContext _dbContext;

    public CreateBookingHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var clientExists = await _dbContext.Users.AnyAsync(user => user.Id == request.ClientId, cancellationToken);
        var photographerExists = await _dbContext.Photographers.AnyAsync(p => p.Id == request.PhotographerId, cancellationToken);

        if (!clientExists || !photographerExists)
        {
            throw new InvalidOperationException("InvalidClientOrPhotographer");
        }

        var start = request.ScheduledAt;
        var end = request.ScheduledAt.AddMinutes(request.DurationMinutes);

        var hasConflict = await _dbContext.Bookings
            .AnyAsync(booking =>
                booking.PhotographerId == request.PhotographerId
                && booking.Status != BookingStatus.Cancelled
                && booking.ScheduledAt < end
                && booking.ScheduledAt.AddMinutes(booking.DurationMinutes) > start,
                cancellationToken);

        if (hasConflict)
        {
            throw new InvalidOperationException("BookingConflict");
        }

        var booking = new Booking
        {
            ClientId = request.ClientId,
            PhotographerId = request.PhotographerId,
            ScheduledAt = request.ScheduledAt,
            DurationMinutes = request.DurationMinutes,
            Notes = request.Notes,
            Price = request.Price,
            Status = BookingStatus.Pending
        };

        _dbContext.Bookings.Add(booking);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new BookingDto
        {
            Id = booking.Id,
            ClientId = booking.ClientId,
            PhotographerId = booking.PhotographerId,
            ScheduledAt = booking.ScheduledAt,
            DurationMinutes = booking.DurationMinutes,
            Status = booking.Status,
            Notes = booking.Notes,
            Price = booking.Price
        };
    }
}
