using Microsoft.EntityFrameworkCore.Query;
using Shared.Core.Common;
using System.Linq.Expressions;

namespace Shared.Core.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false);
    }
}
