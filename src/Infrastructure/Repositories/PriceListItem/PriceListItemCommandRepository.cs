using Delex_POS.Application.Common.Interfaces.Repositories.PricelistItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceListItem;

public class PriceListItemCommandRepository : CommandHandlerBase<Domain.Entities.PriceListItem>, IPriceListItemCommandRepository
{
    public PriceListItemCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
