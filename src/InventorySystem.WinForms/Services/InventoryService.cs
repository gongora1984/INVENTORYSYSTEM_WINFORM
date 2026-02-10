using Microsoft.EntityFrameworkCore;
using InventorySystem.Shared;
using InventorySystem.Infrastructure.Data;

namespace InventorySystem.WinForms.Services;

public class InventoryService : IInventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product?> GetProductByBarcodeAsync(string barcode)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<StockMovement> CreateMovementAsync(StockMovement movement)
    {
        var product = await _context.Products.FindAsync(movement.ProductId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        movement.Date = DateTime.Now;
        _context.StockMovements.Add(movement);

        switch (movement.Type)
        {
            case MovementType.In:
                {
                    if (movement.UnitCost.HasValue)
                    {
                        // Weighted Average Cost Calculation
                        // NewAvg = ((CurrentStock * AvgCost) + (Qty * (NewCost + Transport))) / (CurrentStock + Qty)
                        decimal costPerUnit = movement.UnitCost.Value + (movement.TransportationCost ?? 0);
                        var totalValue = (product.CurrentStock * product.AverageCost) + (movement.Quantity * costPerUnit);
                        var newTotalStock = product.CurrentStock + movement.Quantity;

                        if (newTotalStock > 0)
                        {
                            decimal rawAvg = totalValue / newTotalStock;
                            product.AverageCost = Math.Ceiling(rawAvg * 100) / 100;
                        }
                    }

                    if (movement.ExpirationDate.HasValue)
                    {
                        product.ExpirationDate = movement.ExpirationDate.Value;
                    }

                    product.CurrentStock += movement.Quantity;
                    break;
                }

            case MovementType.Out:
                {
                    if (product.CurrentStock < movement.Quantity)
                    {
                        throw new Exception($"Insufficient stock. Current: {product.CurrentStock}, Requested: {movement.Quantity}");
                    }
                    product.CurrentStock -= movement.Quantity;
                    break;
                }

            case MovementType.Adjustment:
                {
                    product.CurrentStock += movement.Quantity;
                    break;
                }

            default:
                throw new Exception("Invalid movement type.");
        }

        await _context.SaveChangesAsync();
        return movement;
    }

    public async Task<IEnumerable<StockMovement>> GetDailySalesAsync(DateTime date)
    {
        var start = date.Date;
        var end = start.AddDays(1);

        return await _context.StockMovements
            .Include(sm => sm.Product)
            .Where(sm => sm.Type == MovementType.Out && sm.Date >= start && sm.Date < end)
            .OrderByDescending(sm => sm.Date)
            .ToListAsync();
    }
}
