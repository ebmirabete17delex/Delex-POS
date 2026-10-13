using Delex_POS.Application.Common.Interfaces.Repositories.UserHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.UserHistory;

public class UserHistoryCommandRepository : CommandHandlerBase<Domain.Entities.UserHistory>, IUserHistoryCommandRepository
{
    public UserHistoryCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
