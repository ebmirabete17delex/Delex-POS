using Delex_POS.Application.Common.Interfaces.Repositories.PurchaseOrder;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PurchaseOrder;

public class PurchaseOrderQueryRepository : QueryHandlerBase<Domain.Entities.PurchaseOrder>, IPurchaseOrderQueryRepository
{
    public PurchaseOrderQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
