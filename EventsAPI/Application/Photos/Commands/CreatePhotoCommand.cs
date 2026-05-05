using EventsAPI.Application.Photos.Models;
using MediatR;

namespace EventsAPI.Application.Photos.Commands;

public class CreatePhotoCommand : IRequest<PhotoDto>
{
    public Guid BookingId { get; set; }
    public Guid PhotographerId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsSelectedByClient { get; set; }
}
