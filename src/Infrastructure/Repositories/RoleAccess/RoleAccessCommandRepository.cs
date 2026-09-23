using Delex_POS.Infrastructure.Data;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
namespace Delex_POS.Infrastructure.Repositories.RoleAccess;
public class RoleAccessCommandRepository : CommandHandlerBase<Domain.Entities.RBAC.RoleAccess>, IRoleAccessCommandRepository
{
    public RoleAccessCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
