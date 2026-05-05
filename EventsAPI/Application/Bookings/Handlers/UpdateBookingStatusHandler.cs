using EventsAPI.Application.Bookings.Commands;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Bookings.Handlers;

public class UpdateBookingStatusHandler : IRequestHandler<UpdateBookingStatusCommand, bool>
{
    private readonly AppDbContext _dbContext;

    public UpdateBookingStatusHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateBookingStatusCommand request, CancellationToken cancellationToken)
    {
        var booking = await _dbContext.Bookings
            .FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken);

        if (booking is null)
        {
            return false;
        }

        booking.Status = request.Status;
        booking.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
