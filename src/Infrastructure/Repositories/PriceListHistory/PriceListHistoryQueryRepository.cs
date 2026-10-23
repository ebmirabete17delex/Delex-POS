using Delex_POS.Application.Common.Interfaces.Repositories.PriceListHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceListHistory;

public class PriceListHistoryQueryRepository : QueryHandlerBase<Domain.Entities.PriceListHistory>, IPriceListHistoryQueryRepository
{
    public PriceListHistoryQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
