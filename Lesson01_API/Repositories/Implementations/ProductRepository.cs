using Lesson01_API.Data;
using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;

namespace Lesson01_API.Repositories.Implementations
{
    public class ProductRepository : EfRepository<Product, int>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
