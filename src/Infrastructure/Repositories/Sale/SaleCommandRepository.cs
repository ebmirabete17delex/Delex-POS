using Delex_POS.Application.Common.Interfaces.Repositories.Sale;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Sale;

public class SaleCommandRepository : 
    CommandHandlerBase<Domain.Entities.Sale>, 
    ISaleCommandRepository
{
    public SaleCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
