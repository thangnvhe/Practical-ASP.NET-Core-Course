using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Implementations
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _uow;

        public OrderDetailService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync(params Expression<Func<OrderDetail, object>>[] includeProperties)
        {
            return await _uow.OrderDetails.FindAll(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync(Expression<Func<OrderDetail, bool>> predicate, params Expression<Func<OrderDetail, object>>[] includeProperties)
        {
            return await _uow.OrderDetails.FindAll(predicate, includeProperties).ToListAsync();
        }

        public async Task<OrderDetail> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<OrderDetail, object>>[] includeProperties)
        {
            return await _uow.OrderDetails.FindByIdAsync(id, cancellationToken, includeProperties);
        }

        public async Task<OrderDetail> GetSingleAsync(Expression<Func<OrderDetail, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<OrderDetail, object>>[] includeProperties)
        {
            return await _uow.OrderDetails.FindSingleAsync(predicate, cancellationToken, includeProperties);
        }

        public async Task AddAsync(OrderDetail entity)
        {
            _uow.OrderDetails.Add(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderDetail entity)
        {
            _uow.OrderDetails.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _uow.OrderDetails.RemoveAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
