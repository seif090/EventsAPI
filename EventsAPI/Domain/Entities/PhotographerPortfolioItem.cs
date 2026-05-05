namespace EventsAPI.Domain.Entities;

public class PhotographerPortfolioItem : BaseEntity
{
    public Guid PhotographerId { get; set; }
    public Photographer Photographer { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
