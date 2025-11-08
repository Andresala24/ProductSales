using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;

namespace Sales.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Autentica un usuario y genera un token JWT
    /// </summary>
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _authService.LoginAsync(loginDto);
        
        if (response == null)
            return Unauthorized("Usuario o contraseña incorrectos");

        return Ok(response);
    }
}

