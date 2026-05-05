namespace EventsAPI.Domain.Entities;

public class BoxType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
