namespace Domain.Entities;

public class InventoryProduct
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public string? Image { get; set; }
    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
}

