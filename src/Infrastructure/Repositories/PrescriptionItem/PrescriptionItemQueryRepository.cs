using Delex_POS.Application.Common.Interfaces.Repositories.PrescriptionItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PrescriptionItem;

public class PrescriptionItemQueryRepository : QueryHandlerBase<Domain.Entities.PrescriptionItem>, IPrescriptionItemQueryRepository
{
    public PrescriptionItemQueryRepository(ApplicationDbContext dbContext): base(dbContext) { }
}
