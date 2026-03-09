using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(params Expression<Func<Product, object>>[] includeProperties)
        {
            return await _uow.Products.FindAll(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate, params Expression<Func<Product, object>>[] includeProperties)
        {
            return await _uow.Products.FindAll(predicate, includeProperties).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Product, object>>[] includeProperties)
        {
            return await _uow.Products.FindByIdAsync(id, cancellationToken, includeProperties);
        }

        public async Task<Product> GetSingleAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Product, object>>[] includeProperties)
        {
            return await _uow.Products.FindSingleAsync(predicate, cancellationToken, includeProperties);
        }

        public async Task AddAsync(Product entity)
        {
            _uow.Products.Add(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _uow.Products.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _uow.Products.RemoveAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
