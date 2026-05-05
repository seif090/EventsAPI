using EventsAPI.Application.Photos.Commands;
using EventsAPI.Application.Photos.Models;
using EventsAPI.Domain.Entities;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Photos.Handlers;

public class CreatePhotoHandler : IRequestHandler<CreatePhotoCommand, PhotoDto>
{
    private readonly AppDbContext _dbContext;

    public CreatePhotoHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PhotoDto> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
    {
        var bookingExists = await _dbContext.Bookings.AnyAsync(b => b.Id == request.BookingId, cancellationToken);
        var photographerExists = await _dbContext.Photographers.AnyAsync(p => p.Id == request.PhotographerId, cancellationToken);

        if (!bookingExists || !photographerExists)
        {
            throw new InvalidOperationException("InvalidBookingOrPhotographer");
        }

        var photo = new Photo
        {
            BookingId = request.BookingId,
            PhotographerId = request.PhotographerId,
            Url = request.Url,
            IsSelectedByClient = request.IsSelectedByClient
        };

        _dbContext.Photos.Add(photo);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PhotoDto
        {
            Id = photo.Id,
            BookingId = photo.BookingId,
            PhotographerId = photo.PhotographerId,
            Url = photo.Url,
            IsSelectedByClient = photo.IsSelectedByClient
        };
    }
}
