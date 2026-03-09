using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(params Expression<Func<Order, object>>[] includeProperties)
        {
            return await _uow.Orders.FindAll(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate, params Expression<Func<Order, object>>[] includeProperties)
        {
            return await _uow.Orders.FindAll(predicate, includeProperties).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Order, object>>[] includeProperties)
        {
            return await _uow.Orders.FindByIdAsync(id, cancellationToken, includeProperties);
        }

        public async Task<Order> GetSingleAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Order, object>>[] includeProperties)
        {
            return await _uow.Orders.FindSingleAsync(predicate, cancellationToken, includeProperties);
        }

        public async Task AddAsync(Order entity)
        {
            _uow.Orders.Add(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            _uow.Orders.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _uow.Orders.RemoveAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
