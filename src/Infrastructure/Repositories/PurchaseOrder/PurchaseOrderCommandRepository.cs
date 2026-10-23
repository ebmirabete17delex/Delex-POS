using Delex_POS.Application.Common.Interfaces.Repositories.PurchaseOrder;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PurchaseOrder;

public class PurchaseOrderCommandRepository : CommandHandlerBase<Domain.Entities.PurchaseOrder>, IPurchaseOrderCommandRepository
{
    public PurchaseOrderCommandRepository(ApplicationDbContext dbContext): base(dbContext) { }
}
