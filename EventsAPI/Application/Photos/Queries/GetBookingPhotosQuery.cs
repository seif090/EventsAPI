using EventsAPI.Application.Photos.Models;
using MediatR;

namespace EventsAPI.Application.Photos.Queries;

public class GetBookingPhotosQuery : IRequest<IReadOnlyCollection<PhotoDto>>
{
    public Guid BookingId { get; set; }
}
