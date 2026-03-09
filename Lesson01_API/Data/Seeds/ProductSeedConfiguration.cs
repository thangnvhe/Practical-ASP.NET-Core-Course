using Lesson01_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lesson01_API.Data.Seeds
{
    public class ProductSeedConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(GetProducts());
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product>();

            products.Add(new Product { ProductID = 1, Name = "Laptop Model A", Description = "Lightweight laptop", Price = 1200.00m, StandardCost = 800.00m, IsActive = true, Quantity = 10, CategoryId = 2, SupplierId = 2 });
            products.Add(new Product { ProductID = 2, Name = "Smartphone X", Description = "Latest smartphone", Price = 899.99m, StandardCost = 600.00m, IsActive = true, Quantity = 25, CategoryId = 1, SupplierId = 1 });
            products.Add(new Product { ProductID = 3, Name = "Blender 3000", Description = "High-speed blender", Price = 99.50m, StandardCost = 55.00m, IsActive = true, Quantity = 40, CategoryId = 3, SupplierId = 3 });
            products.Add(new Product { ProductID = 4, Name = "Novel - The Journey", Description = "Fiction book", Price = 14.99m, StandardCost = 6.00m, IsActive = true, Quantity = 100, CategoryId = 4, SupplierId = 3 });
            products.Add(new Product { ProductID = 5, Name = "T-Shirt Classic", Description = "Cotton T-Shirt", Price = 19.99m, StandardCost = 5.00m, IsActive = true, Quantity = 200, CategoryId = 5, SupplierId = 3 });

            return products;
        }
    }
}