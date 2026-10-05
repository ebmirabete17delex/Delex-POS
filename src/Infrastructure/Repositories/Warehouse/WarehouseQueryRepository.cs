using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Warehouse;

public class WarehouseQueryRepository : QueryHandlerBase<Domain.Entities.Warehouse>, IWarehouseQueryRepository
{
    public WarehouseQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
