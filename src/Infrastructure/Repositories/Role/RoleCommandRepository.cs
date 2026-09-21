using Delex_POS.Application.Common.Interfaces.Repositories.Role;
using Delex_POS.Infrastructure.Data;
namespace Delex_POS.Infrastructure.Repositories.Role;
public class RoleCommandRepository : CommandHandlerBase<Domain.Entities.RBAC.Role>, IRoleCommandRepository
{
    public RoleCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
