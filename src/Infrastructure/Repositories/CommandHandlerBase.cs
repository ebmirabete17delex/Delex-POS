using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Delex_POS.Infrastructure.Repositories;

public abstract class CommandHandlerBase<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;

    protected CommandHandlerBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync([id], cancellationToken: cancellationToken);

        Guard.Against.NotFound(id, entity);

        _dbContext.Set<TEntity>().Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
