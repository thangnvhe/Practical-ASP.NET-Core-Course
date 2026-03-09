using Lesson01_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lesson01_API.Data.Seeds
{
    public class OrderSeedConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(GetOrders());
        }

        private static List<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order { OrderId = 1, CustomerID = 1, OrderDate = DateTime.Parse("2025-01-02"), Status = "Pending", TotalAmount = 1214.99m, ShippingAddress = "100 Main St", BillingAddress = "100 Main St", Promode = "Standard", ShippedDate = DateTime.Parse("2025-01-03"), DeliveredDate = DateTime.Parse("2025-01-05") },
                new Order { OrderId = 2, CustomerID = 2, OrderDate = DateTime.Parse("2025-02-10"), Status = "Shipped", TotalAmount = 899.99m, ShippingAddress = "101 Main St", BillingAddress = "101 Main St", Promode = "Express", ShippedDate = DateTime.Parse("2025-02-11"), DeliveredDate = DateTime.Parse("2025-02-13") }
            };
        }
    }
}