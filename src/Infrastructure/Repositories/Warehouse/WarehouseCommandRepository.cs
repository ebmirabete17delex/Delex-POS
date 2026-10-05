using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Warehouse;

public class WarehouseCommandRepository : CommandHandlerBase<Domain.Entities.Warehouse>, IWarehouseCommandRepository
{
    public WarehouseCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
