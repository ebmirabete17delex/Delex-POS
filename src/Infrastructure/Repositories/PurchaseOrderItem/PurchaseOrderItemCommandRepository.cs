using Delex_POS.Application.Common.Interfaces.Repositories.PurchaseOrderItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PurchaseOrderItem;

public class PurchaseOrderItemCommandRepository : 
    CommandHandlerBase<Domain.Entities.PurchaseOrderItem>, 
    IPurchaseOrderItemCommandRepository
{
    public PurchaseOrderItemCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
