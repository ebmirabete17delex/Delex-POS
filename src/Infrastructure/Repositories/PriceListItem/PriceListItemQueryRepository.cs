using Delex_POS.Application.Common.Interfaces.Repositories.PricelistItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceListItem;

public class PriceListItemQueryRepository : QueryHandlerBase<Domain.Entities.PriceListItem>, IPriceListItemQueryRepository
{
    public PriceListItemQueryRepository(ApplicationDbContext dbContext): base(dbContext) { }
}
