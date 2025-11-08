using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;

namespace Sales.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SaleController : ControllerBase
{
    private readonly ISaleService _service;

    public SaleController(ISaleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todas las ventas
    /// </summary>
    [HttpGet]
    [Route("GetList")]
    public async Task<ActionResult<List<Sale>>> GetAll()
    {
        var sales = await _service.GetAllAsync();
        return Ok(sales);
    }

    /// <summary>
    /// Obtiene una venta por su ID
    /// </summary>
    [HttpGet]
    [Route("GetById")]
    public async Task<ActionResult<Sale>> GetById(int id)
    {
        var sale = await _service.GetByIdAsync(id);
        if (sale == null)
            return NotFound($"Venta con ID {id} no encontrada");

        return Ok(sale);
    }

    /// <summary>
    /// Crea una nueva venta
    /// </summary>
    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult<Sale>> Create([FromBody] CreateSaleDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var sale = await _service.CreateAsync(createDto);
            return Created($"/api/Sale/GetById?id={sale.Id}", sale);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al crear la venta: {ex.Message}");
        }
    }

    /// <summary>
    /// Elimina una venta
    /// </summary>
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound($"Venta con ID {id} no encontrada");

        return NoContent();
    }

    /// <summary>
    /// Obtiene ventas por rango de fechas
    /// </summary>
    [HttpGet]
    [Route("GetByDateRange")]
    public async Task<ActionResult<List<Sale>>> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        try
        {
            var sales = await _service.GetByDateRangeAsync(startDate, endDate);
            return Ok(sales);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener ventas: {ex.Message}");
        }
    }
}

