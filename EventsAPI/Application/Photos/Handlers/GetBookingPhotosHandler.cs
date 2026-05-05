using EventsAPI.Application.Photos.Models;
using EventsAPI.Application.Photos.Queries;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Photos.Handlers;

public class GetBookingPhotosHandler : IRequestHandler<GetBookingPhotosQuery, IReadOnlyCollection<PhotoDto>>
{
    private readonly AppDbContext _dbContext;

    public GetBookingPhotosHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PhotoDto>> Handle(GetBookingPhotosQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Photos
            .Where(photo => photo.BookingId == request.BookingId)
            .OrderBy(photo => photo.CreatedAt)
            .Select(photo => new PhotoDto
            {
                Id = photo.Id,
                BookingId = photo.BookingId,
                PhotographerId = photo.PhotographerId,
                Url = photo.Url,
                IsSelectedByClient = photo.IsSelectedByClient
            })
            .ToListAsync(cancellationToken);
    }
}
