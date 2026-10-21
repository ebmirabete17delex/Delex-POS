using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.RolePermission;

public class RolePermissionCommandRepository : CommandHandlerBase<Domain.Entities.RolePermission>, IRolePermissionCommandRepository
{
    public RolePermissionCommandRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
