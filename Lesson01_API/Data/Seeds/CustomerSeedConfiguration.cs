using Lesson01_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lesson01_API.Data.Seeds
{
    public class CustomerSeedConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(GetCustomers());
        }

        private static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "555-0100", Address = "100 Main St" },
                new Customer { CustomerID = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "555-0101", Address = "101 Main St" },
                new Customer { CustomerID = 3, FirstName = "Sam", LastName = "Green", Email = "sam.green@example.com", PhoneNumber = "555-0102", Address = "102 Main St" }
            };
        }
    }
}