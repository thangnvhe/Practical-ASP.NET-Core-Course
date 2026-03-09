using Lesson01_API.Data;
using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;

namespace Lesson01_API.Repositories.Implementations
{
    public class CategoryRepository : EfRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
