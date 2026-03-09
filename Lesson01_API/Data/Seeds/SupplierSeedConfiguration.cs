using Lesson01_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lesson01_API.Data.Seeds
{
    public class SupplierSeedConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasData(GetSuppliers());
        }

        private static List<Supplier> GetSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier { SupplierID = 1, SupplierName = "Acme Corp", ContactName = "Alice Smith", Phone = "123-456-7890", Email = "alice@acme.example", Address = "123 Acme Way", IsActive = true },
                new Supplier { SupplierID = 2, SupplierName = "Global Tech", ContactName = "Bob Jones", Phone = "234-567-8901", Email = "bob@globaltech.example", Address = "456 Tech Drive", IsActive = true },
                new Supplier { SupplierID = 3, SupplierName = "Home Goods Co", ContactName = "Carol White", Phone = "345-678-9012", Email = "carol@homegoods.example", Address = "789 Home St", IsActive = true }
            };
        }
    }
}