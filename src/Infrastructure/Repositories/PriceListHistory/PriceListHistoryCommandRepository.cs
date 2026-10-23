using Delex_POS.Application.Common.Interfaces.Repositories.PriceListHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceListHistory;

public class PriceListHistoryCommandRepository : CommandHandlerBase<Domain.Entities.PriceListHistory>, IPriceListHistoryCommandRepository
{
    public PriceListHistoryCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
