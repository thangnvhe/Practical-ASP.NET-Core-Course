using Lesson01_API.Data;
using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;

namespace Lesson01_API.Repositories.Implementations
{
    public class OrderRepository : EfRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
