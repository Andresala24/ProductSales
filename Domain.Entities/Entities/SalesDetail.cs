namespace Domain.Entities;

public class SalesDetail
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public virtual InventoryProduct Product { get; set; } = null!;
    public virtual Sale Sale { get; set; } = null!;
}

