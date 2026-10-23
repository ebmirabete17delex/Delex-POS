using Delex_POS.Application.Common.Interfaces.Repositories.Inventory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Inventory;

public class InventoryQueryRepository : QueryHandlerBase<Domain.Entities.Inventory>, IInventoryQueryRepository
{
    public InventoryQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
