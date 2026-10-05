using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.CancelReason;

public class CancelReasonCommandRepository : CommandHandlerBase<Domain.Entities.CancelReason>, ICancelReasonCommandRepository
{
    public CancelReasonCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
