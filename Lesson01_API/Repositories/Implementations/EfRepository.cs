using Lesson01_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lesson01_API.Repositories.Implementations
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>, IDisposable
        where TEntity : class // Ràng buộc TEntity phải là một class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        // Constructor nhận vào DbContext
        public EfRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> FindAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _dbSet.Where(predicate);
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Nếu có include thêm các bảng khác, ta phải rẽ nhánh query
            if (includeProperties != null && includeProperties.Any())
            {
                var items = FindAll(includeProperties);
                // Giả định khóa chính tên là "Id" để đối chiếu
                return await items.FirstOrDefaultAsync(e => EF.Property<TKey>(e, "Id").Equals(id), cancellationToken);
            }

            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await FindAll(predicate, includeProperties).FirstOrDefaultAsync(cancellationToken);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task RemoveAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await FindByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void RemoveMultiple(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

