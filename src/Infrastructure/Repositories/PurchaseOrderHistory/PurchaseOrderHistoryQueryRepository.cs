using Delex_POS.Application.Common.Interfaces.Repositories.PurchaseOrderHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PurchaseOrderHistory;

public class PurchaseOrderHistoryQueryRepository : 
    QueryHandlerBase<Domain.Entities.PurchaseOrderHistory>, 
    IPurchaseOrderHistoryQueryRepository
{
    public PurchaseOrderHistoryQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
