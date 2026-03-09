using Lesson01_API.Models;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync(params Expression<Func<Category, object>>[] includeProperties);
        Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate, params Expression<Func<Category, object>>[] includeProperties);
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Category, object>>[] includeProperties);
        Task<Category> GetSingleAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Category, object>>[] includeProperties);
        Task AddAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task RemoveAsync(int id);
    }
}
