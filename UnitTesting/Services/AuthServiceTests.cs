using Application.DTOs;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTesting.Services;

/// <summary>
/// Pruebas unitarias para AuthService
/// 
/// CONCEPTOS BÁSICOS:
/// - [Fact]: Marca un método como una prueba unitaria
/// - Arrange: Preparar los datos necesarios para la prueba
/// - Act: Ejecutar el método que queremos probar
/// - Assert: Verificar que el resultado es el esperado
/// </summary>
public class AuthServiceTests
{
    /// <summary>
    /// Prueba que un usuario válido puede iniciar sesión correctamente
    /// </summary>
    [Fact]
    public async Task LoginAsync_UsuarioValido_RetornaToken()
    {
        // Arrange (Preparar)
        var configuration = CreateMockConfiguration();
        var authService = new AuthService(configuration);
        var loginDto = new LoginDto
        {
            Username = "pruebaindigo",
            Password = "pruebaindigo12345"
        };

        // Act (Ejecutar)
        var result = await authService.LoginAsync(loginDto);

        // Assert (Verificar)
        Assert.NotNull(result); // Verifica que el resultado no sea null
        Assert.Equal("pruebaindigo", result.Username); // Verifica que el username sea correcto
        Assert.NotNull(result.Token); // Verifica que se generó un token
        Assert.NotEmpty(result.Token); // Verifica que el token no esté vacío
    }

    /// <summary>
    /// Prueba que un usuario con contraseña incorrecta no puede iniciar sesión
    /// </summary>
    [Fact]
    public async Task LoginAsync_ContrasenaIncorrecta_RetornaNull()
    {
        // Arrange
        var configuration = CreateMockConfiguration();
        var authService = new AuthService(configuration);
        var loginDto = new LoginDto
        {
            Username = "pruebaindigo",
            Password = "passwordIncorrecta"
        };

        // Act
        var result = await authService.LoginAsync(loginDto);

        // Assert
        Assert.Null(result); // Verifica que el resultado sea null cuando las credenciales son incorrectas
    }

    /// <summary>
    /// Prueba que un usuario que no existe no puede iniciar sesión
    /// </summary>
    [Fact]
    public async Task LoginAsync_UsuarioNoExiste_RetornaNull()
    {
        // Arrange
        var configuration = CreateMockConfiguration();
        var authService = new AuthService(configuration);
        var loginDto = new LoginDto
        {
            Username = "usuarioInexistente",
            Password = "cualquierPassword"
        };

        // Act
        var result = await authService.LoginAsync(loginDto);

        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Prueba que todos los usuarios válidos pueden iniciar sesión
    /// </summary>
    [Theory] // [Theory] permite ejecutar la misma prueba con diferentes datos
    [InlineData("pruebaindigo", "pruebaindigo12345")]
    [InlineData("usuario", "usuario123")]
    [InlineData("test", "test123")]
    public async Task LoginAsync_UsuariosValidos_RetornanToken(string username, string password)
    {
        // Arrange
        var configuration = CreateMockConfiguration();
        var authService = new AuthService(configuration);
        var loginDto = new LoginDto
        {
            Username = username,
            Password = password
        };

        // Act
        var result = await authService.LoginAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(username, result.Username);
        Assert.NotNull(result.Token);
    }

    /// <summary>
    /// Crea una configuración mock para las pruebas
    /// </summary>
    private IConfiguration CreateMockConfiguration()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "JwtSettings:SecretKey", "MiClaveSecretaSuperSegura12345678901234567890" },
            { "JwtSettings:Issuer", "SalesAPI" },
            { "JwtSettings:Audience", "SalesClient" },
            { "JwtSettings:ExpirationMinutes", "60" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();
    }
}

