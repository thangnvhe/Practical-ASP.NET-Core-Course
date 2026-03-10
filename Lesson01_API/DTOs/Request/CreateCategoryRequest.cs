namespace Lesson01_API.DTOs.Request
{
    public class CreateCategoryRequest
    {
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryID { get; set; }
    }
}
