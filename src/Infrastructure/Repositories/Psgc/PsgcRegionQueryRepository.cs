using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Psgc;

public class PsgcRegionQueryRepository : QueryHandlerBase<Domain.Entities.PsgcRegion>, IPsgcRegionQueryRepository
{
    public PsgcRegionQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
