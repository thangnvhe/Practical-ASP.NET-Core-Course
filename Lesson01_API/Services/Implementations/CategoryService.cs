using Lesson01_API.Models;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lesson01_API.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(params Expression<Func<Category, object>>[] includeProperties)
        {
            return await _uow.Categories.FindAll(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate, params Expression<Func<Category, object>>[] includeProperties)
        {
            return await _uow.Categories.FindAll(predicate, includeProperties).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Category, object>>[] includeProperties)
        {
            return await _uow.Categories.FindByIdAsync(id, cancellationToken, includeProperties);
        }

        public async Task<Category> GetSingleAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<Category, object>>[] includeProperties)
        {
            return await _uow.Categories.FindSingleAsync(predicate, cancellationToken, includeProperties);
        }

        public async Task AddAsync(Category entity)
        {
            _uow.Categories.Add(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            _uow.Categories.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _uow.Categories.RemoveAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
