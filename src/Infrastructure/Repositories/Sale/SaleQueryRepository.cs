using Delex_POS.Application.Common.Interfaces.Repositories.Sale;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Sale;

public class SaleQueryRepository : 
    QueryHandlerBase<Domain.Entities.Sale>, 
    ISaleQueryRepository
{
    public SaleQueryRepository(ApplicationDbContext dbContext): base(dbContext) { }
}
