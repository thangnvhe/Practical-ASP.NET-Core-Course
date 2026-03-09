using Lesson01_API.Data;
using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;

namespace Lesson01_API.Repositories.Implementations
{
    public class CustomerRepository : EfRepository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
