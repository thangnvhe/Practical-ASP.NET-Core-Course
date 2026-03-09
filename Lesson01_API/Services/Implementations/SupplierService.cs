using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _uow;

        public SupplierService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync(params Expression<Func<Supplier, object>>[] includeProperties)
        {
            return await _uow.Suppliers.FindAll(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync(Expression<Func<Supplier, bool>> predicate, params Expression<Func<Supplier, object>>[] includeProperties)
        {
            return await _uow.Suppliers.FindAll(predicate, includeProperties).ToListAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Supplier, object>>[] includeProperties)
        {
            return await _uow.Suppliers.FindByIdAsync(id, cancellationToken, includeProperties);
        }

        public async Task<Supplier> GetSingleAsync(Expression<Func<Supplier, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Supplier, object>>[] includeProperties)
        {
            return await _uow.Suppliers.FindSingleAsync(predicate, cancellationToken, includeProperties);
        }

        public async Task AddAsync(Supplier entity)
        {
            _uow.Suppliers.Add(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Supplier entity)
        {
            _uow.Suppliers.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _uow.Suppliers.RemoveAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
