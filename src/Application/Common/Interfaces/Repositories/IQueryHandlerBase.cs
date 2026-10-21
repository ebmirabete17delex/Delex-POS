using System.Linq.Expressions;
using Delex_POS.Domain.Common;

namespace Delex_POS.Application.Common.Interfaces.Repositories;

public interface IQueryHandlerBase<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    IQueryable<TEntity> GetAll();
}
