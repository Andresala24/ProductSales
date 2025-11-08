using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Application.Interfaces;
using Application.DTOs;

namespace Sales.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InventoryProductController : ControllerBase
{
    private readonly IInventoryProductService _service;
    private readonly IBlobStorageService _blobStorageService;

    public InventoryProductController(IInventoryProductService service, IBlobStorageService blobStorageService)
    {
        _service = service;
        _blobStorageService = blobStorageService;
    }

    /// <summary>
    /// Obtiene todos los productos del inventario
    /// </summary>
    [HttpGet]
    [Route("GetList")]
    public async Task<ActionResult<List<InventoryProduct>>> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Obtiene un producto del inventario por su ID
    /// </summary>
    [HttpGet]
    [Route("GetById")]
    public async Task<ActionResult<InventoryProduct>> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound($"Producto con ID {id} no encontrado");

        return Ok(product);
    }

    /// <summary>
    /// Crea un nuevo producto en el inventario
    /// </summary>
    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult<InventoryProduct>> Create([FromForm] CreateInventoryProductDto createDto, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (imageFile != null && imageFile.Length > 0)
        {
            try
            {
                var imageUrl = await _blobStorageService.UploadImageAsync(imageFile, imageFile.FileName);
                createDto.Image = imageUrl;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al subir la imagen: {ex.Message}");
            }
        }

        try
        {
            var product = await _service.CreateAsync(createDto);
            return Created($"/api/InventoryProduct/GetById?id={product.Id}", product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al crear el producto: {ex.Message}");
        }
    }

    /// <summary>
    /// Actualiza un producto del inventario
    /// </summary>
    [HttpPut]
    [Route("Update")]
    public async Task<ActionResult<InventoryProduct>> Update(int id, [FromForm] UpdateInventoryProductDto updateDto, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingProduct = await _service.GetByIdAsync(id);
        if (existingProduct == null)
            return NotFound($"Producto con ID {id} no encontrado");

        if (imageFile != null && imageFile.Length > 0)
        {
            try
            {
                var imageUrl = await _blobStorageService.UploadImageAsync(
                    imageFile, 
                    imageFile.FileName, 
                    existingProduct.Image);
                updateDto.Image = imageUrl;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al subir la imagen: {ex.Message}");
            }
        }

        try
        {
            var product = await _service.UpdateAsync(id, updateDto);
            if (product == null)
                return NotFound($"Producto con ID {id} no encontrado");

            return Ok(product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al actualizar el producto: {ex.Message}");
        }
    }

    /// <summary>
    /// Elimina un producto del inventario
    /// </summary>
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound($"Producto con ID {id} no encontrado");

        return NoContent();
    }
}

