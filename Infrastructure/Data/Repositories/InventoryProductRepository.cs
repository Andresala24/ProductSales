using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Infrastructure.Data;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories;

public class InventoryProductRepository : IInventoryProductRepository
{
    private readonly PruebaIndigoContext _context;

    public InventoryProductRepository(PruebaIndigoContext context)
    {
        _context = context;
    }

    public async Task<InventoryProduct?> GetByIdAsync(int id)
    {
        return await _context.InventoryProducts.FindAsync(id);
    }

    public async Task<IEnumerable<InventoryProduct>> GetAllAsync()
    {
        return await _context.InventoryProducts.ToListAsync();
    }

    public async Task<IEnumerable<InventoryProduct>> FindAsync(Expression<Func<InventoryProduct, bool>> predicate)
    {
        return await _context.InventoryProducts.Where(predicate).ToListAsync();
    }

    public async Task<InventoryProduct> AddAsync(InventoryProduct entity)
    {
        _context.InventoryProducts.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(InventoryProduct entity)
    {
        _context.InventoryProducts.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(InventoryProduct entity)
    {
        _context.InventoryProducts.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

