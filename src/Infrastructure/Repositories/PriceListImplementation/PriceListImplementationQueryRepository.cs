using Delex_POS.Application.Common.Interfaces.Repositories.PriceListImplementation;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceListImplementation;

public class PriceListImplementationQueryRepository : 
    QueryHandlerBase<Domain.Entities.PriceListImplementation>, 
    IPriceListImplementationQueryRepository
{
    public PriceListImplementationQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
