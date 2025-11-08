using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces;

public interface ISaleService
{
    Task<List<Sale>> GetAllAsync();
    Task<Sale?> GetByIdAsync(int id);
    Task<Sale> CreateAsync(CreateSaleDto createDto);
    Task<bool> DeleteAsync(int id);
    Task<List<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}

