using Lesson01_API.Models;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync(params Expression<Func<Customer, object>>[] includeProperties);
        Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>> predicate, params Expression<Func<Customer, object>>[] includeProperties);
        Task<Customer> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Customer, object>>[] includeProperties);
        Task<Customer> GetSingleAsync(Expression<Func<Customer, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Customer, object>>[] includeProperties);
        Task AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task RemoveAsync(int id);
    }
}
