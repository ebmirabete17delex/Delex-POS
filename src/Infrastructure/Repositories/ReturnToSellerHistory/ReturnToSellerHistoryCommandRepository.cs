using Delex_POS.Application.Common.Interfaces.Repositories.ReturnToSellerHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ReturnToSellerHistory;

public class ReturnToSellerHistoryCommandRepository : 
    CommandHandlerBase<Domain.Entities.ReturnToSellerHistory>, 
    IReturnToSellerHistoryCommandRepository
{
    public ReturnToSellerHistoryCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
