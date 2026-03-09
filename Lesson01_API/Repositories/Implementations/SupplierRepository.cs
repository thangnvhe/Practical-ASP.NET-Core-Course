using Lesson01_API.Data;
using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;

namespace Lesson01_API.Repositories.Implementations
{
    public class SupplierRepository : EfRepository<Supplier, int>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context)
        {
        }
    }
}
