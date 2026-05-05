namespace EventsAPI.Domain.Entities;

public class AlbumType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public decimal BasePrice { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
