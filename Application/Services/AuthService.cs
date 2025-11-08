using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    // Usuarios hardcodeados para simplificar
    private readonly Dictionary<string, string> _users = new()
    {
        { "pruebaindigo", "pruebaindigo12345" },
        { "usuario", "usuario123" },
        { "test", "test123" }
    };

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
    {
        if (!_users.ContainsKey(loginDto.Username) || 
            _users[loginDto.Username] != loginDto.Password)
        {
            return Task.FromResult<LoginResponseDto?>(null);
        }

        var token = GenerateJwtToken(loginDto.Username);
        var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");
        
        var response = new LoginResponseDto
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes),
            Username = loginDto.Username
        };

        return Task.FromResult<LoginResponseDto?>(response);
    }

    private string GenerateJwtToken(string username)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"] 
            ?? throw new InvalidOperationException("JWT SecretKey no configurada");
        var issuer = _configuration["JwtSettings:Issuer"] 
            ?? throw new InvalidOperationException("JWT Issuer no configurado");
        var audience = _configuration["JwtSettings:Audience"] 
            ?? throw new InvalidOperationException("JWT Audience no configurado");
        var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

