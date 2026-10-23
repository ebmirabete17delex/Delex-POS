using Delex_POS.Application.Common.Interfaces.Repositories.PurchaseOrderHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PurchaseOrderHistory;

public class PurchaseOrderHistoryCommandRepository : 
    CommandHandlerBase<Domain.Entities.PurchaseOrderHistory>, 
    IPurchaseOrderHistoryCommandRepository
{
    public PurchaseOrderHistoryCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
