using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Core.Common;
using System.Linq.Expressions;

namespace Shared.Core.Repositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
        where TContext : DbContext
    {
        private readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task CreateAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => Table.Remove(entity));
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>().AsNoTracking();
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>().AsNoTracking();
            if (include is not null) queryable = include(queryable);
            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _context.Set<TEntity>().Update(entity));
            await _context.SaveChangesAsync();
        }
    }
}
