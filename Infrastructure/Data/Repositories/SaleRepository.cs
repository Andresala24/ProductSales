using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Infrastructure.Data;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly PruebaIndigoContext _context;

    public SaleRepository(PruebaIndigoContext context)
    {
        _context = context;
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        return await _context.Sales.FindAsync(id);
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales.ToListAsync();
    }

    public async Task<IEnumerable<Sale>> GetAllWithDetailsAsync()
    {
        return await _context.Sales
            .Include(s => s.SalesDetails)
                .ThenInclude(sd => sd.Product)
            .ToListAsync();
    }

    public async Task<Sale?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Sales
            .Include(s => s.SalesDetails)
                .ThenInclude(sd => sd.Product)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        // Asegurar que endDate incluya todo el día (hasta las 23:59:59)
        var endDateInclusive = endDate.Date.AddDays(1).AddTicks(-1);
        
        return await _context.Sales
            .Include(s => s.SalesDetails)
                .ThenInclude(sd => sd.Product)
            .Where(s => s.CreationDate >= startDate.Date && s.CreationDate <= endDateInclusive)
            .OrderBy(s => s.CreationDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> FindAsync(Expression<Func<Sale, bool>> predicate)
    {
        return await _context.Sales.Where(predicate).ToListAsync();
    }

    public async Task<Sale> AddAsync(Sale entity)
    {
        _context.Sales.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Sale entity)
    {
        _context.Sales.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Sale entity)
    {
        // Eliminar detalles primero
        _context.SalesDetails.RemoveRange(entity.SalesDetails);
        _context.Sales.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

