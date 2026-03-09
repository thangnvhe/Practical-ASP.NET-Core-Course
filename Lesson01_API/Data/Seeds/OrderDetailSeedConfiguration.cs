using Lesson01_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lesson01_API.Data.Seeds
{
    public class OrderDetailSeedConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasData(GetOrderDetails());
        }

        private static List<OrderDetail> GetOrderDetails()
        {
            return new List<OrderDetail>
            {
                new OrderDetail { OrderDetailID = 1, OrderID = 1, ProductID = 1, Quantity = 1, UnitPrice = 1200, Discount = 0 },
                new OrderDetail { OrderDetailID = 2, OrderID = 1, ProductID = 4, Quantity = 1, UnitPrice = 14, Discount = 0 },
                new OrderDetail { OrderDetailID = 3, OrderID = 2, ProductID = 2, Quantity = 1, UnitPrice = 899, Discount = 0 }
            };
        }
    }
}