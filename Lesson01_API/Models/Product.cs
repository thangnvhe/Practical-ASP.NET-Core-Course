namespace Lesson01_API.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal StandardCost { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
