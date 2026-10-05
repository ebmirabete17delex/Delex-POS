using Delex_POS.Application.Common.Interfaces.Repositories.Region;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Region;

public class RegionCommandRepository : CommandHandlerBase<Domain.Entities.Region>, IRegionCommandRepository
{
    public RegionCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
