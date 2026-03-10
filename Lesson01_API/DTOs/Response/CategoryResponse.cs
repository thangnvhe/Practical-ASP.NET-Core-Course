namespace Lesson01_API.DTOs.Response
{
    public class CategoryResponse
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryID { get; set; }
        public string? ParentCategoryName { get; set; }
    }
}
