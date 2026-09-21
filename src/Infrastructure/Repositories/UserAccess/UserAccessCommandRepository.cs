using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
using Delex_POS.Infrastructure.Data;
namespace Delex_POS.Infrastructure.Repositories.UserAccess;
public class UserAccessCommandRepository : CommandHandlerBase<Domain.Entities.RBAC.UserAccess>, IUserAccessCommandRepository
{
    public UserAccessCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
