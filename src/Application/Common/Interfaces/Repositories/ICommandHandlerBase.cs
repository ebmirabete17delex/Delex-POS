namespace Delex_POS.Application.Common.Interfaces.Repositories;

public interface ICommandHandlerBase<TEntity> where TEntity : class
{
    Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
