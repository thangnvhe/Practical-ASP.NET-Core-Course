using Lesson01_API.Models;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Interfaces
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetail>> GetAllAsync(params Expression<Func<OrderDetail, object>>[] includeProperties);
        Task<IEnumerable<OrderDetail>> GetAllAsync(Expression<Func<OrderDetail, bool>> predicate, params Expression<Func<OrderDetail, object>>[] includeProperties);
        Task<OrderDetail> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<OrderDetail, object>>[] includeProperties);
        Task<OrderDetail> GetSingleAsync(Expression<Func<OrderDetail, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<OrderDetail, object>>[] includeProperties);
        Task AddAsync(OrderDetail entity);
        Task UpdateAsync(OrderDetail entity);
        Task RemoveAsync(int id);
    }
}
