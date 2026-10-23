using Delex_POS.Application.Common.Interfaces.Repositories.TransferRequestHistory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TransferRequestHistory;

public class TransferRequestHistoryCommandRepository : 
    CommandHandlerBase<Domain.Entities.TransferRequestHistory>, 
    ITransferRequestHistoryCommandRepository
{
    public TransferRequestHistoryCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
