using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderLine> OrderLines { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<OrderLine>()
            .HasOne(o => o.Order)
            .WithMany(o => o.Lines)
            .HasForeignKey(o => o.OrderId);

        modelBuilder.Entity<OrderLine>()
            .HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId);
    }
}