using AutoMapper;
using Lesson01_API.DTOs.Common;
using Lesson01_API.DTOs.Request;
using Lesson01_API.DTOs.Response;
using Lesson01_API.Exceptions;
using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lesson01_API.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _uow.Categories.FindAll(c=>c.ParentCategory!).AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<PagedResult<CategoryResponse>> GetPagedAsync(CategoryFilterRequest request, CancellationToken cancellationToken)
        {
            var query = _uow.Categories.FindAll(c=>c.ParentCategory!);

            // 1. LỌC DỮ LIỆU (Search)
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.CategoryName.Contains(request.Search));
            }

            // 2. ĐẾM TỔNG SỐ LƯỢNG (Đếm ngay sau khi lọc, trước khi sắp xếp để tối ưu hiệu năng)
            int totalItems = await query.CountAsync(cancellationToken);

            // Nếu không có dữ liệu nào khớp với Search, thoát luôn cho nhanh, đỡ phải xuống DB chạy OrderBy và Skip/Take
            if (totalItems == 0)
            {
                return new PagedResult<CategoryResponse>(new List<CategoryResponse>(), 0, request.Page, request.Size);
            }

            // 3. SẮP XẾP (Sorting)
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                query = request.IsDescending
                    ? query.OrderByDescending(c => EF.Property<object>(c, request.SortBy))
                    : query.OrderBy(c => EF.Property<object>(c, request.SortBy));
            }
            else
            {
                // QUAN TRỌNG: Sắp xếp mặc định để tránh lỗi SQL Server khi Skip/Take
                query = query.OrderByDescending(c => c.CategoryID); // Hoặc c.CategoryId tùy bạn đặt tên
            }

            // 4. PHÂN TRANG (Paging)
            var items = await query
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToListAsync(cancellationToken);

            // 5. MAP & TRẢ VỀ
            var responseItems = _mapper.Map<IEnumerable<CategoryResponse>>(items);

            return new PagedResult<CategoryResponse>(responseItems, totalItems, request.Page, request.Size);
        }

        public async Task<CategoryResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _uow.Categories.FindByIdAsync(id, cancellationToken,c=>c.ParentCategory!);
            if (category == null)
            {
                // Cái AppException này sẽ bị GlobalExceptionHandler tóm gọn và biến thành lỗi 404 chuẩn chỉ!
                throw new AppException(ErrorCodes.EntityNotFound("Category", id));
            }
            var response = _mapper.Map<CategoryResponse>(category);
            return response;
        }

        public async Task<CategoryResponse> AddAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Kiểm tra an toàn (Defensive programming)
            if (request == null)
            {
                throw new AppException(ErrorCodes.ValidationError("Request body cannot be null."));
            }
            if (request.ParentCategoryID == 0) request.ParentCategoryID = null;
            // Nếu có ParentCategoryID, kiểm tra xem nó có tồn tại không để tránh lỗi FK khi lưu xuống DB
            if (request.ParentCategoryID.HasValue)
            {
                var parentCategory = await _uow.Categories.FindByIdAsync(request.ParentCategoryID.Value, cancellationToken);
                if (parentCategory == null)
                {
                    throw new AppException(ErrorCodes.EntityNotFound("Parent Category", request.ParentCategoryID.Value));
                }
            }

            // 2. Map DTO -> Entity (Lúc này mới sinh ra entity)
            var category = _mapper.Map<Category>(request);

            // 3. Thêm vào Context (Thao tác trên RAM)
            _uow.Categories.Add(category);

            // 4. Lưu xuống DB (Truyền token vào đây để phòng trường hợp user hủy request)
            await _uow.SaveChangesAsync(cancellationToken);

            // 5. Map ngược lại Entity (đã có ID mới) -> Response DTO
            var response = _mapper.Map<CategoryResponse>(category);

            return response;
        }

        public async Task<CategoryResponse> UpdateAsync(int id, UpdateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var category = await _uow.Categories.FindByIdAsync(id, cancellationToken);
            if (category == null)
            {
                throw new AppException(ErrorCodes.EntityNotFound("Category", id));
            }
            if (request.ParentCategoryID == 0) request.ParentCategoryID = null;
            // Nếu có ParentCategoryID, kiểm tra xem nó có tồn tại và khác Id hiện tại không để tránh lỗi FK khi lưu xuống DB
            if (request.ParentCategoryID.HasValue && request.ParentCategoryID != 0)
            {
                if (request.ParentCategoryID.Value == id)
                {
                    throw new AppException(ErrorCodes.ValidationError("Parent Category cannot be the same as the category being updated."));
                }
                var parentCategory = await _uow.Categories.FindByIdAsync(request.ParentCategoryID.Value, cancellationToken);
                if (parentCategory == null)
                {
                    throw new AppException(ErrorCodes.EntityNotFound("Parent Category", request.ParentCategoryID.Value));
                }
            }
            var updatedCategory = _mapper.Map(request, category);
            _uow.Categories.Update(updatedCategory);
            await _uow.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<CategoryResponse>(updatedCategory);
            return response;
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _uow.Categories.FindByIdAsync(id, cancellationToken);
            if (category == null)
            {
                throw new AppException(ErrorCodes.EntityNotFound("Category", id));
            }
          
            var hasChildCategories = await _uow.Categories.FindAll(c => c.ParentCategoryID == id).AnyAsync(cancellationToken);
            if(hasChildCategories)
            {
                throw new AppException(ErrorCodes.ValidationError("Cannot delete category because it has child categories. Please delete or reassign child categories first."));
            } 
            _uow.Categories.Remove(category);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
