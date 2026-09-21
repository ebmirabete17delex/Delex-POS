using System.Linq.Expressions;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories;

public abstract class QueryHandlerBase<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;

    protected QueryHandlerBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync([id], cancellationToken)
            .ConfigureAwait(false);

        return entity;
    }
    
    public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().AllAsync<TEntity>(predicate, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().ToListAsync<TEntity>(cancellationToken).ConfigureAwait(false);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().AsNoTracking<TEntity>();
    }
}
