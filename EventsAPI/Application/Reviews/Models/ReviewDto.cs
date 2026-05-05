namespace EventsAPI.Application.Reviews.Models;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid PhotographerId { get; set; }
    public Guid ClientId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}
