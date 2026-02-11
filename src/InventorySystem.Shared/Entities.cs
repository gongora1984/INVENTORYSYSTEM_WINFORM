using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Shared;

public enum MovementType
{
    In,
    Out,
    Adjustment
}

public enum UserRole
{
    Admin,
    Seller
}

public class User
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password PIN is required")]
    public string PasswordPin { get; set; } = string.Empty;
    
    public UserRole Role { get; set; }
}

public class Product
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Barcode is required")]
    public string Barcode { get; set; } = string.Empty;
    
    public decimal AverageCost { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal SellingPrice { get; set; }
    
    public int CurrentStock { get; set; }
    public DateTime? ExpirationDate { get; set; }
}

public class StockMovement
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal? UnitCost { get; set; } // Cost at time of entry (for calculating average)
    public decimal? TransportationCost { get; set; } // Extra cost per unit
    public DateTime? ExpirationDate { get; set; }
    public string? OrderNumber { get; set; } // Unique Order ID
    public MovementType Type { get; set; }
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
    
    // Navigation property
    public virtual Product? Product { get; set; }
}
