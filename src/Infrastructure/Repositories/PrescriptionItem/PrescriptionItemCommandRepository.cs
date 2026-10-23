using Delex_POS.Application.Common.Interfaces.Repositories.PrescriptionItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PrescriptionItem;

public class PrescriptionItemCommandRepository : CommandHandlerBase<Domain.Entities.PrescriptionItem>, IPrescriptionItemCommandRepository
{
    public PrescriptionItemCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
