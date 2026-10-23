using Delex_POS.Application.Common.Interfaces.Repositories.Inventory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Inventory;

public class InventoryCommandRepository : CommandHandlerBase<Domain.Entities.Inventory>, IInventoryCommandRepository
{
    public InventoryCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
