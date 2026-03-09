namespace Lesson01_API.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryID { get; set; }
        public ICollection<Product>? Products { get; set; }
        public Category? ParentCategory { get; set; }
    }
}
