using Delex_POS.Application.Common.Interfaces.Repositories.PriceListType;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceListType;

public class PriceListTypeQueryRepository : QueryHandlerBase<Domain.Entities.PriceListType>, IPriceListTypeQueryRepository
{
    public PriceListTypeQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
