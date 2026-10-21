using Delex_POS.Application.Common.Interfaces.Repositories.Permission;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Permission;

public class PermissionCommandRepository : CommandHandlerBase<Domain.Entities.Permission>, IPermissionCommandRepository
{
    public PermissionCommandRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
