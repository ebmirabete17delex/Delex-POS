using Delex_POS.Application.Common.Interfaces.Repositories.InventoryTransaction;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.InventoryTransaction;

public class InventoryTransactionCommandRepository : CommandHandlerBase<Domain.Entities.InventoryTransaction>, IInventoryTransactionCommandRepository
{
    public InventoryTransactionCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
