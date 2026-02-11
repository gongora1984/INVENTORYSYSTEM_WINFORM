using Microsoft.EntityFrameworkCore;
using InventorySystem.Shared;

namespace InventorySystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.SellingPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(p => p.AverageCost)
            .HasPrecision(18, 2);

        modelBuilder.Entity<StockMovement>()
            .Property(sm => sm.UnitCost)
            .HasPrecision(18, 2);

        modelBuilder.Entity<StockMovement>()
            .Property(sm => sm.TransportationCost)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Barcode)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        
        // Initial seeding for Admin and Seller
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("A1B2C3D4-E5F6-4A0B-8C9D-0E1F2A3B4C5D"),
                Username = "admin",
                PasswordPin = SecurityService.Encrypt("8604"),
                Role = UserRole.Admin
            },
            new User
            {
                Id = Guid.Parse("B2C3D4E5-F6A7-4B1C-9D0E-1F2A3B4C5D6E"),
                Username = "seller",
                PasswordPin = SecurityService.Encrypt("1234"),
                Role = UserRole.Seller
            }
        );
    }
}
