using Delex_POS.Application.Common.Interfaces.Repositories.User;
using Delex_POS.Infrastructure.Data;
namespace Delex_POS.Infrastructure.Repositories.User;
public class UserCommandRepository : CommandHandlerBase<Domain.Entities.RBAC.User>, IUserCommandRepository
{
    public UserCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
