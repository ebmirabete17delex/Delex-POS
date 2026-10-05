using Delex_POS.Application.Common.Interfaces.Repositories.Region;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Region;

public class RegionQueryRepository : QueryHandlerBase<Domain.Entities.Region>, IRegionQueryRepository
{
    public RegionQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
