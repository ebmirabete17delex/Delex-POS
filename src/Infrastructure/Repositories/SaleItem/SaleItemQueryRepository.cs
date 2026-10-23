using Delex_POS.Application.Common.Interfaces.Repositories.SaleItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.SaleItem;

public class SaleItemQueryRepository : 
    QueryHandlerBase<Domain.Entities.SaleItem>, 
    ISaleItemQueryRepository
{
    public SaleItemQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
