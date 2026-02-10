using InventorySystem.Shared;

namespace InventorySystem.WinForms.Services;

public interface IInventoryService
{
    // Product Management
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductAsync(Guid id);
    Task<Product?> GetProductByBarcodeAsync(string barcode);
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);

    // Stock & Movements
    Task<StockMovement> CreateMovementAsync(StockMovement movement);
    Task<IEnumerable<StockMovement>> GetDailySalesAsync(DateTime date);
}
