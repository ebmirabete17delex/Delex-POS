using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Account;

public class AccountQueryRepository : QueryHandlerBase<Domain.Entities.Account>, IAccountQueryRepository
{
    public AccountQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
