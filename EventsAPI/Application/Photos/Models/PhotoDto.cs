namespace EventsAPI.Application.Photos.Models;

public class PhotoDto
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid PhotographerId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsSelectedByClient { get; set; }
}
