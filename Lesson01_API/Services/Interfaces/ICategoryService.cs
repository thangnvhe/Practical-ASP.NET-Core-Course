using Lesson01_API.DTOs.Common;
using Lesson01_API.DTOs.Request;
using Lesson01_API.DTOs.Response;

namespace Lesson01_API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PagedResult<CategoryResponse>> GetPagedAsync(CategoryFilterRequest request, CancellationToken cancellationToken = default);
        Task<CategoryResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CategoryResponse> AddAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default);
        Task<CategoryResponse> UpdateAsync(int id, UpdateCategoryRequest request, CancellationToken cancellationToken = default);
        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
    }
}
