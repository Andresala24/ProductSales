using Domain.Entities;

namespace Domain.Entities.Interfaces;

public interface IInventoryProductRepository : IRepository<InventoryProduct>
{
    // Método disponible para futuras necesidades si se requiere cargar productos con sus detalles de venta
    // Task<IEnumerable<InventoryProduct>> GetAllWithDetailsAsync();
}

