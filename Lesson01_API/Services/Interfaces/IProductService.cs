using Lesson01_API.Models;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(params Expression<Func<Product, object>>[] includeProperties);
        Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate, params Expression<Func<Product, object>>[] includeProperties);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Product, object>>[] includeProperties);
        Task<Product> GetSingleAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Product, object>>[] includeProperties);
        Task AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task RemoveAsync(int id);
    }
}
