using Delex_POS.Application.Common.Interfaces.Repositories.PriceListType;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceListType;

public class PriceListTypeCommandRepository : CommandHandlerBase<Domain.Entities.PriceListType>, IPriceListTypeCommandRepository
{
    public PriceListTypeCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
