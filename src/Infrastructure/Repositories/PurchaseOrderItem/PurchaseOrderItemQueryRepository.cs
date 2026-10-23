using Delex_POS.Application.Common.Interfaces.Repositories.PurchaseOrderItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PurchaseOrderItem;

public class PurchaseOrderItemQueryRepository : 
    QueryHandlerBase<Domain.Entities.PurchaseOrderItem>, 
    IPurchaseOrderItemQueryRepository
{
    public PurchaseOrderItemQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
