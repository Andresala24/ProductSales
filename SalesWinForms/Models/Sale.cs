namespace SalesWinForms.Models;

public class Sale
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreationUser { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public List<SalesDetail> SalesDetails { get; set; } = new();
}

public class SalesDetail
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public InventoryProduct? Product { get; set; }
}

public class CreateSaleDto
{
    public string CreationUser { get; set; } = string.Empty;
    public List<SalesDetailDto> SalesDetails { get; set; } = new();
}

public class SalesDetailDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

