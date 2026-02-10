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
        
        // No seed data for production standalone
    }
}
