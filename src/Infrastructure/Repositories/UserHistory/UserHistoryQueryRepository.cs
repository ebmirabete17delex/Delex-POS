using Delex_POS.Application.Common.Interfaces.Repositories.UserHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.UserHistory;

public class UserHistoryQueryRepository : QueryHandlerBase<Domain.Entities.UserHistory>, IUserHistoryQueryRepository
{
    public UserHistoryQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
