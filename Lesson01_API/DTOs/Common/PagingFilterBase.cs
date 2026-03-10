namespace Lesson01_API.DTOs.Common
{
    public abstract class PagingFilterBase
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
    }
}
