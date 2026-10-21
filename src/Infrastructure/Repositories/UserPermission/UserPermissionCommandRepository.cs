using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.UserPermission;

public class UserPermissionCommandRepository : CommandHandlerBase<Domain.Entities.UserPermission>, IUserPermissionCommandRepository
{
    public UserPermissionCommandRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
