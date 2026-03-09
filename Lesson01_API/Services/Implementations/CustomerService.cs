using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;

        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(params Expression<Func<Customer, object>>[] includeProperties)
        {
            return await _uow.Customers.FindAll(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>> predicate, params Expression<Func<Customer, object>>[] includeProperties)
        {
            return await _uow.Customers.FindAll(predicate, includeProperties).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Customer, object>>[] includeProperties)
        {
            return await _uow.Customers.FindByIdAsync(id, cancellationToken, includeProperties);
        }

        public async Task<Customer> GetSingleAsync(Expression<Func<Customer, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Customer, object>>[] includeProperties)
        {
            return await _uow.Customers.FindSingleAsync(predicate, cancellationToken, includeProperties);
        }

        public async Task AddAsync(Customer entity)
        {
            _uow.Customers.Add(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer entity)
        {
            _uow.Customers.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _uow.Customers.RemoveAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
