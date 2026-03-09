using Lesson01_API.Models;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync(params Expression<Func<Order, object>>[] includeProperties);
        Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate, params Expression<Func<Order, object>>[] includeProperties);
        Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Order, object>>[] includeProperties);
        Task<Order> GetSingleAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Order, object>>[] includeProperties);
        Task AddAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task RemoveAsync(int id);
    }
}
