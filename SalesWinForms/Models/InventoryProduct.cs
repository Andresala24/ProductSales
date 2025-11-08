namespace SalesWinForms.Models;

public class InventoryProduct
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public string? Image { get; set; }
}

public class CreateInventoryProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public string? Image { get; set; }
}

public class UpdateInventoryProductDto
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public string? Image { get; set; }
}

