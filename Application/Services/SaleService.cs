using Domain.Entities;
using Domain.Entities.Interfaces;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IInventoryProductRepository _productRepository;

    public SaleService(
        ISaleRepository saleRepository,
        IInventoryProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        var sales = await _saleRepository.GetAllWithDetailsAsync();
        return sales.ToList();
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        return await _saleRepository.GetByIdWithDetailsAsync(id);
    }

    public async Task<Sale> CreateAsync(CreateSaleDto createDto)
    {
        if (createDto.SalesDetails == null || !createDto.SalesDetails.Any())
            throw new ArgumentException("La venta debe tener al menos un detalle");

        var sale = new Sale
        {
            CreationDate = DateTime.Now,
            CreationUser = createDto.CreationUser
        };

        var detallesPorProducto = createDto.SalesDetails
            .GroupBy(d => d.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                CantidadTotal = g.Sum(d => d.Quantity),
                Detalles = g.ToList()
            })
            .ToList();

        foreach (var grupo in detallesPorProducto)
        {
            var product = await _productRepository.GetByIdAsync(grupo.ProductId);
            if (product == null)
                throw new ArgumentException($"El producto con ID {grupo.ProductId} no existe");

            if (product.Stock.HasValue)
            {
                if (grupo.CantidadTotal > product.Stock.Value)
                {
                    throw new ArgumentException(
                        $"Stock insuficiente para el producto '{product.Name}'. " +
                        $"Stock disponible: {product.Stock.Value}, " +
                        $"Cantidad solicitada: {grupo.CantidadTotal}");
                }

                product.Stock = product.Stock.Value - grupo.CantidadTotal;
                await _productRepository.UpdateAsync(product);
            }

            foreach (var detailDto in grupo.Detalles)
            {
                var detail = new SalesDetail
                {
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    UnitPrice = detailDto.UnitPrice
                };

                sale.AddDetail(detail);
            }
        }

        return await _saleRepository.AddAsync(sale);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sale = await _saleRepository.GetByIdWithDetailsAsync(id);
        if (sale == null)
            return false;

        await _saleRepository.DeleteAsync(sale);
        return true;
    }

    public async Task<List<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin");

        var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
        return sales.ToList();
    }
}

