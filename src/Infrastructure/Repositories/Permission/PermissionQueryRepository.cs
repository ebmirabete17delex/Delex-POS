using Delex_POS.Application.Common.Interfaces.Repositories.Permission;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Permission;

public class PermissionQueryRepository : QueryHandlerBase<Domain.Entities.Permission>, IPermissionQueryRepository
{
    public PermissionQueryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
