using System.Linq.Expressions;

namespace Delex_POS.Application.Common.Interfaces.Repositories;

public interface IQueryHandlerBase<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    IQueryable<TEntity> GetAll();
}
