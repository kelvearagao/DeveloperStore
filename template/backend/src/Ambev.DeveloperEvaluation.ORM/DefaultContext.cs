using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; } 
    

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    
        // Seed Users
        var user1Id = Guid.NewGuid();
        var user2Id = Guid.NewGuid();

        modelBuilder.Entity<User>().HasData(
            new User { Id = user1Id, Username = "John Doe", Email = "john.doe@example.com", Password = "Ab#123123" },
            new User { Id = user2Id, Username = "Jane Smith", Email = "jane.smith@example.com", Password =  "Ab#123123" }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Title = "Product A", Price = 10.99m, Category = "Category A", Description = "Product A Description", Image = "product-a.jpg" },
            new Product { Id = 2, Title = "Product B", Price = 20.49m, Category = "Category B" , Description = "Product B Description", Image = "product-b.jpg" }
        );

        // Seed Sales
        var sale1Id = Guid.NewGuid();
        var sale2Id = Guid.NewGuid();

        modelBuilder.Entity<Sale>().HasData(
            new Sale { Id = sale1Id, SaleNumber = 1001, SaleDate = DateTime.UtcNow, UserId = user1Id, TotalAmount = 50.00m, IsCancelled = false, Branch = "Branch A" },
            new Sale { Id = sale2Id, SaleNumber = 1002, SaleDate = DateTime.UtcNow, UserId = user2Id, TotalAmount = 30.00m, IsCancelled = true, Branch = "Branch B" }
        );

        // Seed SaleItems
        modelBuilder.Entity<SaleItem>().HasData(
            new SaleItem { Id = 1, SaleId = sale1Id, ProductId = 1, Quantity = 2, UnitPrice = 10.00m },
            new SaleItem { Id = 2, SaleId = sale1Id, ProductId = 2, Quantity = 1, UnitPrice = 30.00m },
            new SaleItem { Id = 3, SaleId = sale2Id, ProductId = 1, Quantity = 3, UnitPrice = 10.00m }
        );
            
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
        );

        return new DefaultContext(builder.Options);
    }
}