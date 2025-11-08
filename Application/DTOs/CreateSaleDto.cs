using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs;

public class CreateSaleDto
{
    [Required(ErrorMessage = "El usuario de creación es requerido")]
    public string CreationUser { get; set; } = null!;

    [Required(ErrorMessage = "La venta debe tener al menos un detalle")]
    public List<SalesDetailDto> SalesDetails { get; set; } = new();
}

public class SalesDetailDto
{
    [Required(ErrorMessage = "El ID del producto es requerido")]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor o igual a 0")]
    public decimal UnitPrice { get; set; }
}

