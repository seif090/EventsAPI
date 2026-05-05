namespace EventsAPI.Domain.Entities;

public class PhotographerAvailability : BaseEntity
{
    public Guid PhotographerId { get; set; }
    public Photographer Photographer { get; set; } = null!;

    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsBooked { get; set; }
}
