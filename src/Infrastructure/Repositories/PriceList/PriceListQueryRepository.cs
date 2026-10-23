using Delex_POS.Application.Common.Interfaces.Repositories.PriceList;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceList;

public class PriceListQueryRepository : QueryHandlerBase<Domain.Entities.PriceList>, IPriceListQueryRepository
{
    public PriceListQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
