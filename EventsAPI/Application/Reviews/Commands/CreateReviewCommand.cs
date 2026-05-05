using EventsAPI.Application.Reviews.Models;
using MediatR;

namespace EventsAPI.Application.Reviews.Commands;

public class CreateReviewCommand : IRequest<ReviewDto>
{
    public Guid BookingId { get; set; }
    public Guid PhotographerId { get; set; }
    public Guid ClientId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}
