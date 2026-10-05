using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Account;

public class AccountCommandRepository : CommandHandlerBase<Domain.Entities.Account>, IAccountCommandRepository
{
    public AccountCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
