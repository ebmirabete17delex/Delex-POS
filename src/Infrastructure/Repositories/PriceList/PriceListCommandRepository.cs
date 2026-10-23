using Delex_POS.Application.Common.Interfaces.Repositories.PriceList;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceList;

public class PriceListCommandRepository : CommandHandlerBase<Domain.Entities.PriceList>, IPriceListCommandRepository
{
    public PriceListCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
