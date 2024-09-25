using Microsoft.EntityFrameworkCore;
using SmartTech.Models;

namespace SmartTech.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // Corrected to refer to Product

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Smartphone",
                    Description = "Latest model smartphone with all features.",
                    Price = 699.99m,
                    StockQuantity = 50,
                    Category = "Electronics",
                    ImageFile = "smartphone.jpg"
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Laptop",
                    Description = "High performance laptop for gaming and work.",
                    Price = 1299.99m,
                    StockQuantity = 30,
                    Category = "Computers",
                    ImageFile = "laptop.jpg"
                }
            );
        }
    }
}
