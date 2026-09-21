using Delex_POS.Application.Common.Interfaces.Repositories.UserRole;
using Delex_POS.Infrastructure.Data;
namespace Delex_POS.Infrastructure.Repositories.UserRole;
public class UserRoleCommandRepository : CommandHandlerBase<Domain.Entities.RBAC.UserRole>, IUserRoleCommandRepository
{
    public UserRoleCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
