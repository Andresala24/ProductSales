using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CreateInventoryProductDto
{
    [Required(ErrorMessage = "El nombre del producto es requerido")]
    [StringLength(400, ErrorMessage = "El nombre no puede exceder 400 caracteres")]
    public string Name { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0")]
    public decimal? Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
    public int? Stock { get; set; }

    public string? Image { get; set; }
}

