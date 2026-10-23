using Delex_POS.Application.Common.Interfaces.Repositories;
using Delex_POS.Application.Common.Interfaces.Repositories.ReturnToSellerHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ReturnToSellerHistory;

public class ReturnToSellerHistoryQueryRepository : 
    QueryHandlerBase<Domain.Entities.ReturnToSellerHistory>, 
    IReturnToSellerHistoryQueryRepository
{
    public ReturnToSellerHistoryQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
