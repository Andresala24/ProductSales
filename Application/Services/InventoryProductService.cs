using Domain.Entities;
using Domain.Entities.Interfaces;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services;

public class InventoryProductService : IInventoryProductService
{
    private readonly IInventoryProductRepository _repository;
    private readonly IBlobStorageService _blobStorageService;

    public InventoryProductService(
        IInventoryProductRepository repository,
        IBlobStorageService blobStorageService)
    {
        _repository = repository;
        _blobStorageService = blobStorageService;
    }

    public async Task<List<InventoryProduct>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.ToList();
    }

    public async Task<InventoryProduct?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<InventoryProduct> CreateAsync(CreateInventoryProductDto createDto)
    {
        var product = new InventoryProduct
        {
            Name = createDto.Name,
            Price = createDto.Price,
            Stock = createDto.Stock,
            Image = createDto.Image
        };

        return await _repository.AddAsync(product);
    }

    public async Task<InventoryProduct?> UpdateAsync(int id, UpdateInventoryProductDto updateDto)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            return null;

        if (!string.IsNullOrWhiteSpace(updateDto.Name))
            product.Name = updateDto.Name;

        if (updateDto.Price.HasValue)
            product.Price = updateDto.Price;

        if (updateDto.Stock.HasValue)
            product.Stock = updateDto.Stock;

        if (!string.IsNullOrWhiteSpace(updateDto.Image) && !string.IsNullOrWhiteSpace(product.Image))
        {
            if (updateDto.Image != product.Image)
            {
                await _blobStorageService.DeleteImageAsync(product.Image);
            }
        }

        if (updateDto.Image != null)
            product.Image = updateDto.Image;

        await _repository.UpdateAsync(product);
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            return false;

        if (!string.IsNullOrWhiteSpace(product.Image))
        {
            try
            {
                var imageDeleted = await _blobStorageService.DeleteImageAsync(product.Image);
                if (!imageDeleted)
                {
                    System.Diagnostics.Debug.WriteLine($"Advertencia: No se pudo eliminar la imagen del blob storage para el producto {id}. URL: {product.Image}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al eliminar imagen del blob storage para producto {id}: {ex.Message}");
            }
        }

        await _repository.DeleteAsync(product);
        return true;
    }
}

