using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.UnitOfMeasure;

public class UnitOfMeasureCommandRepository : CommandHandlerBase<Domain.Entities.UnitOfMeasure>, IUnitOfMeasureCommandRepository
{
    public UnitOfMeasureCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
