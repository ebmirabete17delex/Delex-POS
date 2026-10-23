using Delex_POS.Application.Common.Interfaces.Repositories.SaleItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.SaleItem;

public class SaleItemCommandRepository : 
    CommandHandlerBase<Domain.Entities.SaleItem>, 
    ISaleItemCommandRepository
{
    public SaleItemCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
