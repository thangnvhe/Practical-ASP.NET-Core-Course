using Lesson01_API.Models;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllAsync(params Expression<Func<Supplier, object>>[] includeProperties);
        Task<IEnumerable<Supplier>> GetAllAsync(Expression<Func<Supplier, bool>> predicate, params Expression<Func<Supplier, object>>[] includeProperties);
        Task<Supplier> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Supplier, object>>[] includeProperties);
        Task<Supplier> GetSingleAsync(Expression<Func<Supplier, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Supplier, object>>[] includeProperties);
        Task AddAsync(Supplier entity);
        Task UpdateAsync(Supplier entity);
        Task RemoveAsync(int id);
    }
}
