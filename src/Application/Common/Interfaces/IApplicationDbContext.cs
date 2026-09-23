using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Entities.RBAC;

namespace Delex_POS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Branch> Branches { get; }

    DbSet<RoleAccess> RoleAccesses { get; }
        
    DbSet<Domain.Entities.RBAC.AccessClaim> Accesses { get; }

    DbSet<UserAccess> UserAccesses { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<TodoList> TodoLists { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
