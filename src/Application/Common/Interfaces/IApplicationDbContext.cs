using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Entities.RBAC;

namespace Delex_POS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<User> POSUsers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
