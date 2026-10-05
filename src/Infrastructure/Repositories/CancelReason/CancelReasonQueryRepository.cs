using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.CancelReason;

public class CancelReasonQueryRepository : QueryHandlerBase<Domain.Entities.CancelReason>, ICancelReasonQueryRepository
{
    public CancelReasonQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
