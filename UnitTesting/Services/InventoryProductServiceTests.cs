using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Moq;
using Xunit;

namespace UnitTesting.Services;

/// <summary>
/// Pruebas unitarias para InventoryProductService
/// 
/// CONCEPTOS AVANZADOS:
/// - Mock: Simula objetos que el servicio necesita pero no queremos probar directamente
/// - Setup: Configura cómo debe comportarse el mock cuando se llama un método
/// - Verify: Verifica que se llamaron los métodos esperados en el mock
/// </summary>
public class InventoryProductServiceTests
{
    private readonly Mock<IInventoryProductRepository> _mockRepository;
    private readonly Mock<IBlobStorageService> _mockBlobStorage;
    private readonly InventoryProductService _service;

    // Constructor: Se ejecuta antes de cada prueba
    public InventoryProductServiceTests()
    {
        _mockRepository = new Mock<IInventoryProductRepository>();
        _mockBlobStorage = new Mock<IBlobStorageService>();
        _service = new InventoryProductService(_mockRepository.Object, _mockBlobStorage.Object);
    }

    /// <summary>
    /// Prueba que GetAllAsync retorna todos los productos
    /// </summary>
    [Fact]
    public async Task GetAllAsync_RetornaListaDeProductos()
    {
        // Arrange
        var productosEsperados = new List<InventoryProduct>
        {
            new InventoryProduct { Id = 1, Name = "Producto 1", Price = 100, Stock = 10 },
            new InventoryProduct { Id = 2, Name = "Producto 2", Price = 200, Stock = 20 }
        };

        // Configurar el mock para que retorne los productos cuando se llame GetAllAsync
        _mockRepository
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(productosEsperados);

        // Act
        var resultado = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
        Assert.Equal("Producto 1", resultado[0].Name);
        Assert.Equal("Producto 2", resultado[1].Name);

        // Verificar que se llamó el método del repositorio
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    /// <summary>
    /// Prueba que GetByIdAsync retorna el producto correcto
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_ProductoExiste_RetornaProducto()
    {
        // Arrange
        var productoEsperado = new InventoryProduct
        {
            Id = 1,
            Name = "Producto Test",
            Price = 150,
            Stock = 5
        };

        _mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(productoEsperado);

        // Act
        var resultado = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(1, resultado.Id);
        Assert.Equal("Producto Test", resultado.Name);
        Assert.Equal(150, resultado.Price);
        Assert.Equal(5, resultado.Stock);

        _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    /// <summary>
    /// Prueba que GetByIdAsync retorna null cuando el producto no existe
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_ProductoNoExiste_RetornaNull()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((InventoryProduct?)null);

        // Act
        var resultado = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(resultado);
        _mockRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
    }

    /// <summary>
    /// Prueba que CreateAsync crea un producto correctamente
    /// </summary>
    [Fact]
    public async Task CreateAsync_DatosValidos_CreaProducto()
    {
        // Arrange
        var createDto = new CreateInventoryProductDto
        {
            Name = "Nuevo Producto",
            Price = 99.99m,
            Stock = 50,
            Image = "https://example.com/imagen.jpg"
        };

        var productoCreado = new InventoryProduct
        {
            Id = 1,
            Name = createDto.Name,
            Price = createDto.Price,
            Stock = createDto.Stock,
            Image = createDto.Image
        };

        _mockRepository
            .Setup(r => r.AddAsync(It.IsAny<InventoryProduct>()))
            .ReturnsAsync(productoCreado);

        // Act
        var resultado = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Nuevo Producto", resultado.Name);
        Assert.Equal(99.99m, resultado.Price);
        Assert.Equal(50, resultado.Stock);
        Assert.Equal("https://example.com/imagen.jpg", resultado.Image);

        // Verificar que se llamó AddAsync con un producto que tiene los datos correctos
        _mockRepository.Verify(r => r.AddAsync(It.Is<InventoryProduct>(p =>
            p.Name == createDto.Name &&
            p.Price == createDto.Price &&
            p.Stock == createDto.Stock &&
            p.Image == createDto.Image
        )), Times.Once);
    }

    /// <summary>
    /// Prueba que UpdateAsync actualiza un producto existente
    /// </summary>
    [Fact]
    public async Task UpdateAsync_ProductoExiste_ActualizaProducto()
    {
        // Arrange
        var productoExistente = new InventoryProduct
        {
            Id = 1,
            Name = "Producto Original",
            Price = 100,
            Stock = 10,
            Image = "https://example.com/imagen-vieja.jpg"
        };

        var updateDto = new UpdateInventoryProductDto
        {
            Name = "Producto Actualizado",
            Price = 150,
            Stock = 20
        };

        _mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(productoExistente);

        _mockRepository
            .Setup(r => r.UpdateAsync(It.IsAny<InventoryProduct>()))
            .Returns(Task.CompletedTask);

        // Act
        var resultado = await _service.UpdateAsync(1, updateDto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Producto Actualizado", resultado.Name);
        Assert.Equal(150, resultado.Price);
        Assert.Equal(20, resultado.Stock);

        _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<InventoryProduct>()), Times.Once);
    }

    /// <summary>
    /// Prueba que UpdateAsync retorna null cuando el producto no existe
    /// </summary>
    [Fact]
    public async Task UpdateAsync_ProductoNoExiste_RetornaNull()
    {
        // Arrange
        var updateDto = new UpdateInventoryProductDto
        {
            Name = "Producto Actualizado"
        };

        _mockRepository
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((InventoryProduct?)null);

        // Act
        var resultado = await _service.UpdateAsync(999, updateDto);

        // Assert
        Assert.Null(resultado);
        _mockRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<InventoryProduct>()), Times.Never);
    }

    /// <summary>
    /// Prueba que UpdateAsync elimina la imagen anterior cuando se actualiza con una nueva
    /// </summary>
    [Fact]
    public async Task UpdateAsync_ActualizaImagen_EliminaImagenAnterior()
    {
        // Arrange
        var productoExistente = new InventoryProduct
        {
            Id = 1,
            Name = "Producto",
            Image = "https://example.com/imagen-vieja.jpg"
        };

        var updateDto = new UpdateInventoryProductDto
        {
            Image = "https://example.com/imagen-nueva.jpg"
        };

        _mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(productoExistente);

        _mockRepository
            .Setup(r => r.UpdateAsync(It.IsAny<InventoryProduct>()))
            .Returns(Task.CompletedTask);

        _mockBlobStorage
            .Setup(b => b.DeleteImageAsync("https://example.com/imagen-vieja.jpg"))
            .ReturnsAsync(true);

        // Act
        var resultado = await _service.UpdateAsync(1, updateDto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("https://example.com/imagen-nueva.jpg", resultado.Image);

        // Verificar que se eliminó la imagen anterior del blob storage
        _mockBlobStorage.Verify(b => b.DeleteImageAsync("https://example.com/imagen-vieja.jpg"), Times.Once);
    }

    /// <summary>
    /// Prueba que DeleteAsync elimina un producto existente
    /// </summary>
    [Fact]
    public async Task DeleteAsync_ProductoExiste_EliminaProducto()
    {
        // Arrange
        var productoExistente = new InventoryProduct
        {
            Id = 1,
            Name = "Producto a Eliminar",
            Image = "https://example.com/imagen.jpg"
        };

        _mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(productoExistente);

        _mockRepository
            .Setup(r => r.DeleteAsync(It.IsAny<InventoryProduct>()))
            .Returns(Task.CompletedTask);

        _mockBlobStorage
            .Setup(b => b.DeleteImageAsync("https://example.com/imagen.jpg"))
            .ReturnsAsync(true);

        // Act
        var resultado = await _service.DeleteAsync(1);

        // Assert
        Assert.True(resultado);
        _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<InventoryProduct>()), Times.Once);
        _mockBlobStorage.Verify(b => b.DeleteImageAsync("https://example.com/imagen.jpg"), Times.Once);
    }

    /// <summary>
    /// Prueba que DeleteAsync retorna false cuando el producto no existe
    /// </summary>
    [Fact]
    public async Task DeleteAsync_ProductoNoExiste_RetornaFalse()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((InventoryProduct?)null);

        // Act
        var resultado = await _service.DeleteAsync(999);

        // Assert
        Assert.False(resultado);
        _mockRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
        _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<InventoryProduct>()), Times.Never);
    }
}

