using Lesson01_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lesson01_API.Data.Seeds
{
    public class CategorySeedConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(GetCategories());
        }

        private static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Electronics", Description = "Electronic devices and gadgets", ParentCategoryID = null },
                new Category { CategoryID = 2, CategoryName = "Computers", Description = "Desktops, laptops and accessories", ParentCategoryID = 1 },
                new Category { CategoryID = 3, CategoryName = "Home", Description = "Home and kitchen appliances", ParentCategoryID = null },
                new Category { CategoryID = 4, CategoryName = "Books", Description = "Printed and digital books", ParentCategoryID = null },
                new Category { CategoryID = 5, CategoryName = "Clothing", Description = "Men and women apparel", ParentCategoryID = null }
            };
        }
    }
}