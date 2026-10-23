using Delex_POS.Application.Common.Interfaces.Repositories.InventoryTransaction;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.InventoryTransaction;

public class InventoryTransactionQueryRepository : QueryHandlerBase<Domain.Entities.InventoryTransaction>, IInventoryTransactionQueryRepository
{
    public InventoryTransactionQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
