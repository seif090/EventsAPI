using EventsAPI.Application.Reviews.Commands;
using EventsAPI.Application.Reviews.Models;
using EventsAPI.Domain.Entities;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Reviews.Handlers;

public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly AppDbContext _dbContext;

    public CreateReviewHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var booking = await _dbContext.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken);

        if (booking is null || booking.ClientId != request.ClientId || booking.PhotographerId != request.PhotographerId)
        {
            throw new InvalidOperationException("InvalidBooking");
        }

        var exists = await _dbContext.Reviews.AnyAsync(r => r.BookingId == request.BookingId, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("ReviewAlreadyExists");
        }

        var review = new Review
        {
            BookingId = request.BookingId,
            PhotographerId = request.PhotographerId,
            ClientId = request.ClientId,
            Rating = request.Rating,
            Comment = request.Comment
        };

        _dbContext.Reviews.Add(review);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ReviewDto
        {
            Id = review.Id,
            BookingId = review.BookingId,
            PhotographerId = review.PhotographerId,
            ClientId = review.ClientId,
            Rating = review.Rating,
            Comment = review.Comment
        };
    }
}
