using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Moq;
using Xunit;

namespace UnitTesting.Services;

/// <summary>
/// Pruebas unitarias para SaleService
/// 
/// CASOS COMPLEJOS:
/// - Validación de reglas de negocio (stock, productos existentes)
/// - Manejo de excepciones
/// - Lógica de agrupación y cálculos
/// </summary>
public class SaleServiceTests
{
    private readonly Mock<ISaleRepository> _mockSaleRepository;
    private readonly Mock<IInventoryProductRepository> _mockProductRepository;
    private readonly SaleService _service;

    public SaleServiceTests()
    {
        _mockSaleRepository = new Mock<ISaleRepository>();
        _mockProductRepository = new Mock<IInventoryProductRepository>();
        _service = new SaleService(_mockSaleRepository.Object, _mockProductRepository.Object);
    }

    /// <summary>
    /// Prueba que CreateAsync crea una venta correctamente cuando hay stock suficiente
    /// </summary>
    [Fact]
    public async Task CreateAsync_StockSuficiente_CreaVenta()
    {
        // Arrange
        var producto = new InventoryProduct
        {
            Id = 1,
            Name = "Producto Test",
            Stock = 100,
            Price = 50
        };

        var createDto = new CreateSaleDto
        {
            CreationUser = "usuario_test",
            SalesDetails = new List<SalesDetailDto>
            {
                new SalesDetailDto
                {
                    ProductId = 1,
                    Quantity = 10,
                    UnitPrice = 50
                }
            }
        };

        var ventaCreada = new Sale
        {
            Id = 1,
            CreationDate = DateTime.Now,
            CreationUser = "usuario_test",
            Total = 500
        };

        _mockProductRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(producto);

        _mockProductRepository
            .Setup(r => r.UpdateAsync(It.IsAny<InventoryProduct>()))
            .Returns(Task.CompletedTask);

        _mockSaleRepository
            .Setup(r => r.AddAsync(It.IsAny<Sale>()))
            .ReturnsAsync(ventaCreada);

        // Act
        var resultado = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("usuario_test", resultado.CreationUser);

        // Verificar que se redujo el stock (100 - 10 = 90)
        _mockProductRepository.Verify(r => r.UpdateAsync(It.Is<InventoryProduct>(p =>
            p.Id == 1 && p.Stock == 90
        )), Times.Once);

        _mockSaleRepository.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
    }

    /// <summary>
    /// Prueba que CreateAsync lanza excepción cuando no hay stock suficiente
    /// </summary>
    [Fact]
    public async Task CreateAsync_StockInsuficiente_LanzaExcepcion()
    {
        // Arrange
        var producto = new InventoryProduct
        {
            Id = 1,
            Name = "Producto Test",
            Stock = 5, // Stock insuficiente
            Price = 50
        };

        var createDto = new CreateSaleDto
        {
            CreationUser = "usuario_test",
            SalesDetails = new List<SalesDetailDto>
            {
                new SalesDetailDto
                {
                    ProductId = 1,
                    Quantity = 10, // Se solicita más de lo disponible
                    UnitPrice = 50
                }
            }
        };

        _mockProductRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(producto);

        // Act & Assert
        var excepcion = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(createDto));
        
        Assert.Contains("Stock insuficiente", excepcion.Message);
        Assert.Contains("Producto Test", excepcion.Message);

        // Verificar que NO se creó la venta
        _mockSaleRepository.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Never);
    }

    /// <summary>
    /// Prueba que CreateAsync lanza excepción cuando el producto no existe
    /// </summary>
    [Fact]
    public async Task CreateAsync_ProductoNoExiste_LanzaExcepcion()
    {
        // Arrange
        var createDto = new CreateSaleDto
        {
            CreationUser = "usuario_test",
            SalesDetails = new List<SalesDetailDto>
            {
                new SalesDetailDto
                {
                    ProductId = 999, // Producto que no existe
                    Quantity = 10,
                    UnitPrice = 50
                }
            }
        };

        _mockProductRepository
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((InventoryProduct?)null);

        // Act & Assert
        var excepcion = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(createDto));
        
        Assert.Contains("no existe", excepcion.Message);
        Assert.Contains("999", excepcion.Message);

        _mockSaleRepository.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Never);
    }

    /// <summary>
    /// Prueba que CreateAsync lanza excepción cuando no hay detalles en la venta
    /// </summary>
    [Fact]
    public async Task CreateAsync_SinDetalles_LanzaExcepcion()
    {
        // Arrange
        var createDto = new CreateSaleDto
        {
            CreationUser = "usuario_test",
            SalesDetails = new List<SalesDetailDto>() // Lista vacía
        };

        // Act & Assert
        var excepcion = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(createDto));
        
        Assert.Contains("al menos un detalle", excepcion.Message);

        _mockSaleRepository.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Never);
    }

    /// <summary>
    /// Prueba que CreateAsync agrupa correctamente múltiples detalles del mismo producto
    /// </summary>
    [Fact]
    public async Task CreateAsync_MultiplesDetallesMismoProducto_AgrupaCorrectamente()
    {
        // Arrange
        var producto = new InventoryProduct
        {
            Id = 1,
            Name = "Producto Test",
            Stock = 100,
            Price = 50
        };

        var createDto = new CreateSaleDto
        {
            CreationUser = "usuario_test",
            SalesDetails = new List<SalesDetailDto>
            {
                new SalesDetailDto { ProductId = 1, Quantity = 5, UnitPrice = 50 },
                new SalesDetailDto { ProductId = 1, Quantity = 10, UnitPrice = 50 }, // Mismo producto
                new SalesDetailDto { ProductId = 1, Quantity = 3, UnitPrice = 50 }  // Mismo producto
            }
        };

        var ventaCreada = new Sale { Id = 1 };

        _mockProductRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(producto);

        _mockProductRepository
            .Setup(r => r.UpdateAsync(It.IsAny<InventoryProduct>()))
            .Returns(Task.CompletedTask);

        _mockSaleRepository
            .Setup(r => r.AddAsync(It.IsAny<Sale>()))
            .ReturnsAsync(ventaCreada);

        // Act
        var resultado = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(resultado);

        // Verificar que el stock se redujo por la cantidad total (5 + 10 + 3 = 18)
        // Stock inicial: 100, Stock final: 100 - 18 = 82
        _mockProductRepository.Verify(r => r.UpdateAsync(It.Is<InventoryProduct>(p =>
            p.Id == 1 && p.Stock == 82
        )), Times.Once);
    }

    /// <summary>
    /// Prueba que GetAllAsync retorna todas las ventas
    /// </summary>
    [Fact]
    public async Task GetAllAsync_RetornaListaDeVentas()
    {
        // Arrange
        var ventasEsperadas = new List<Sale>
        {
            new Sale { Id = 1, CreationUser = "usuario1", Total = 100 },
            new Sale { Id = 2, CreationUser = "usuario2", Total = 200 }
        };

        _mockSaleRepository
            .Setup(r => r.GetAllWithDetailsAsync())
            .ReturnsAsync(ventasEsperadas);

        // Act
        var resultado = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
        _mockSaleRepository.Verify(r => r.GetAllWithDetailsAsync(), Times.Once);
    }

    /// <summary>
    /// Prueba que GetByIdAsync retorna la venta correcta
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_VentaExiste_RetornaVenta()
    {
        // Arrange
        var ventaEsperada = new Sale
        {
            Id = 1,
            CreationUser = "usuario_test",
            Total = 500
        };

        _mockSaleRepository
            .Setup(r => r.GetByIdWithDetailsAsync(1))
            .ReturnsAsync(ventaEsperada);

        // Act
        var resultado = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(1, resultado.Id);
        Assert.Equal("usuario_test", resultado.CreationUser);
        _mockSaleRepository.Verify(r => r.GetByIdWithDetailsAsync(1), Times.Once);
    }

    /// <summary>
    /// Prueba que DeleteAsync elimina una venta existente
    /// </summary>
    [Fact]
    public async Task DeleteAsync_VentaExiste_EliminaVenta()
    {
        // Arrange
        var ventaExistente = new Sale { Id = 1 };

        _mockSaleRepository
            .Setup(r => r.GetByIdWithDetailsAsync(1))
            .ReturnsAsync(ventaExistente);

        _mockSaleRepository
            .Setup(r => r.DeleteAsync(It.IsAny<Sale>()))
            .Returns(Task.CompletedTask);

        // Act
        var resultado = await _service.DeleteAsync(1);

        // Assert
        Assert.True(resultado);
        _mockSaleRepository.Verify(r => r.GetByIdWithDetailsAsync(1), Times.Once);
        _mockSaleRepository.Verify(r => r.DeleteAsync(It.IsAny<Sale>()), Times.Once);
    }

    /// <summary>
    /// Prueba que DeleteAsync retorna false cuando la venta no existe
    /// </summary>
    [Fact]
    public async Task DeleteAsync_VentaNoExiste_RetornaFalse()
    {
        // Arrange
        _mockSaleRepository
            .Setup(r => r.GetByIdWithDetailsAsync(999))
            .ReturnsAsync((Sale?)null);

        // Act
        var resultado = await _service.DeleteAsync(999);

        // Assert
        Assert.False(resultado);
        _mockSaleRepository.Verify(r => r.GetByIdWithDetailsAsync(999), Times.Once);
        _mockSaleRepository.Verify(r => r.DeleteAsync(It.IsAny<Sale>()), Times.Never);
    }

    /// <summary>
    /// Prueba que GetByDateRangeAsync retorna ventas en el rango correcto
    /// </summary>
    [Fact]
    public async Task GetByDateRangeAsync_RangoValido_RetornaVentas()
    {
        // Arrange
        var fechaInicio = new DateTime(2025, 1, 1);
        var fechaFin = new DateTime(2025, 1, 31);

        var ventasEsperadas = new List<Sale>
        {
            new Sale { Id = 1, CreationDate = new DateTime(2025, 1, 15) },
            new Sale { Id = 2, CreationDate = new DateTime(2025, 1, 20) }
        };

        _mockSaleRepository
            .Setup(r => r.GetByDateRangeAsync(fechaInicio, fechaFin))
            .ReturnsAsync(ventasEsperadas);

        // Act
        var resultado = await _service.GetByDateRangeAsync(fechaInicio, fechaFin);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
        _mockSaleRepository.Verify(r => r.GetByDateRangeAsync(fechaInicio, fechaFin), Times.Once);
    }

    /// <summary>
    /// Prueba que GetByDateRangeAsync lanza excepción cuando la fecha inicio es mayor que la fecha fin
    /// </summary>
    [Fact]
    public async Task GetByDateRangeAsync_FechaInicioMayorQueFin_LanzaExcepcion()
    {
        // Arrange
        var fechaInicio = new DateTime(2025, 1, 31);
        var fechaFin = new DateTime(2025, 1, 1); // Fecha fin menor que inicio

        // Act & Assert
        var excepcion = await Assert.ThrowsAsync<ArgumentException>(() => 
            _service.GetByDateRangeAsync(fechaInicio, fechaFin));
        
        Assert.Contains("fecha de inicio no puede ser mayor", excepcion.Message);

        _mockSaleRepository.Verify(r => r.GetByDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Never);
    }
}

