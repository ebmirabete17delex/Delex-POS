using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceLevel;

public class PriceLevelQueryRepository : QueryHandlerBase<Domain.Entities.PriceLevel>, IPriceLevelQueryRepository
{
    public PriceLevelQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
