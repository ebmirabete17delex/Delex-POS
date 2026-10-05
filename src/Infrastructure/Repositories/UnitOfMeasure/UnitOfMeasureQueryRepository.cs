using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.UnitOfMeasure;

public class UnitOfMeasureQueryRepository : QueryHandlerBase<Domain.Entities.UnitOfMeasure>, IUnitOfMeasureQueryRepository
{
    public UnitOfMeasureQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
