using Domain.Entities;

namespace Domain.Entities.Interfaces;

public interface ISaleRepository : IRepository<Sale>
{
    Task<IEnumerable<Sale>> GetAllWithDetailsAsync();
    Task<Sale?> GetByIdWithDetailsAsync(int id);
    Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}

