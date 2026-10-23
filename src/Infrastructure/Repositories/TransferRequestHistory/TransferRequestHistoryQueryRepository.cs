using Delex_POS.Application.Common.Interfaces.Repositories.TransferRequestHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TransferRequestHistory;

public class TransferRequestHistoryQueryRepository : QueryHandlerBase<Domain.Entities.TransferRequestHistory>, ITransferRequestHistoryQueryRepository
{
    public TransferRequestHistoryQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
