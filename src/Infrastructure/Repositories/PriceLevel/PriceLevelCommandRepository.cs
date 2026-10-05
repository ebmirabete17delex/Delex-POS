using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceLevel;

public class PriceLevelCommandRepository : CommandHandlerBase<Domain.Entities.PriceLevel>, IPriceLevelCommandRepository
{
    public PriceLevelCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
