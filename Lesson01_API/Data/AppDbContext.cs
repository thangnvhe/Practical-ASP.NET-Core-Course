using Lesson01_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson01_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply IEntityTypeConfiguration<> seed/config classes from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryID);
                entity.Property(c => c.CategoryName).IsRequired().HasMaxLength(150);
                entity.HasIndex(c => c.CategoryName).IsUnique();
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.HasOne(c => c.ParentCategory)
                      .WithMany()
                      .HasForeignKey(c => c.ParentCategoryID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(s => s.SupplierID);
                entity.Property(s => s.SupplierName).IsRequired().HasMaxLength(200);
                entity.Property(s => s.ContactName).HasMaxLength(150);
                entity.Property(s => s.Phone).HasMaxLength(50);
                entity.Property(s => s.Email).HasMaxLength(200);
                entity.Property(s => s.Address).HasMaxLength(500);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductID);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.StandardCost).HasPrecision(18, 2);
                entity.Property(p => p.IsActive).HasDefaultValue(true);
                entity.Property(p => p.Quantity).HasDefaultValue(0);

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Supplier)
                      .WithMany(s => s.Products)
                      .HasForeignKey(p => p.SupplierId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerID);
                entity.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.LastName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(200);
                entity.Property(c => c.PhoneNumber).HasMaxLength(50);
                entity.Property(c => c.Address).HasMaxLength(500);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.OrderDate).IsRequired();
                entity.Property(o => o.Status).IsRequired().HasMaxLength(50);
                entity.Property(o => o.TotalAmount).HasPrecision(18, 2);
                entity.Property(o => o.ShippingAddress).HasMaxLength(500);
                entity.Property(o => o.BillingAddress).HasMaxLength(500);
                entity.Property(o => o.Promode).HasMaxLength(100);

                entity.HasOne(o => o.Customer)
                      .WithMany(c => c.Orders)
                      .HasForeignKey(o => o.CustomerID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(od => od.OrderDetailID);
                entity.Property(od => od.Quantity).IsRequired();
                entity.Property(od => od.UnitPrice);
                entity.Property(od => od.Discount);

                entity.HasOne(od => od.Order)
                      .WithMany(o => o.OrderDetails)
                      .HasForeignKey(od => od.OrderID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(od => od.Product)
                      .WithMany()
                      .HasForeignKey(od => od.ProductID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
