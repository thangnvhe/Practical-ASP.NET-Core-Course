using Lesson01_API.DTOs;
using Lesson01_API.DTOs.Common;
using Lesson01_API.DTOs.Request;
using Lesson01_API.DTOs.Response;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lesson01_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // 1. LẤY TẤT CẢ (Không phân trang)
        // GET: api/v1/categories/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAllAsync(cancellationToken);

            var response = APIResponse<IEnumerable<CategoryResponse>>.Builder()
                .WithSuccess(true)
                .WithStatusCode(HttpStatusCode.OK)
                .WithResult(result)
                .Build();

            return Ok(response);
        }

        // 2. LẤY DANH SÁCH (Có phân trang & Lọc)
        // GET: api/v1/categories?page=1&size=10&search=IT
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] CategoryFilterRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetPagedAsync(request, cancellationToken);

            var response = APIResponse<PagedResult<CategoryResponse>>.Builder()
                .WithSuccess(true)
                .WithStatusCode(HttpStatusCode.OK)
                .WithResult(result)
                .Build();

            return Ok(response);
        }

        // 3. LẤY CHI TIẾT 1 BẢN GHI
        // GET: api/v1/categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellationToken);

            // Không cần check null ở đây vì Service đã throw AppException nếu không tìm thấy!
            var response = APIResponse<CategoryResponse>.Builder()
                .WithSuccess(true)
                .WithStatusCode(HttpStatusCode.OK)
                .WithResult(result)
                .Build();

            return Ok(response);
        }

        // 4. THÊM MỚI
        // POST: api/v1/categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.AddAsync(request, cancellationToken);

            var response = APIResponse<CategoryResponse>.Builder()
                .WithSuccess(true)
                .WithStatusCode(HttpStatusCode.Created) // Mã 201 cho Tạo mới thành công
                .WithMessage("Tạo danh mục thành công.")
                .WithResult(result)
                .Build();

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        // 5. CẬP NHẬT
        // PUT: api/v1/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.UpdateAsync(id, request, cancellationToken);

            var response = APIResponse<CategoryResponse>.Builder()
                .WithSuccess(true)
                .WithStatusCode(HttpStatusCode.OK)
                .WithMessage("Cập nhật danh mục thành công.")
                .WithResult(result)
                .Build();

            return Ok(response);
        }

        // 6. XÓA
        // DELETE: api/v1/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryService.RemoveAsync(id, cancellationToken);

            var response = APIResponse<object>.Builder()
                .WithSuccess(true)
                .WithStatusCode(HttpStatusCode.OK)
                .WithMessage("Xóa danh mục thành công.")
                .Build();

            return Ok(response);
        }
    }
}
