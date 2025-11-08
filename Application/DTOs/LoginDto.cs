using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es requerida")]
    public string Password { get; set; } = null!;
}

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public string Username { get; set; } = null!;
}

