using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces;

public interface IInventoryProductService
{
    Task<List<InventoryProduct>> GetAllAsync();
    Task<InventoryProduct?> GetByIdAsync(int id);
    Task<InventoryProduct> CreateAsync(CreateInventoryProductDto createDto);
    Task<InventoryProduct?> UpdateAsync(int id, UpdateInventoryProductDto updateDto);
    Task<bool> DeleteAsync(int id);
}

