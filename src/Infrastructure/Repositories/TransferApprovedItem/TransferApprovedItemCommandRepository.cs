using Delex_POS.Application.Common.Interfaces.Repositories.TransferApprovedItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TransferApprovedItem;

public class TransferApprovedItemCommandRepository : 
    CommandHandlerBase<Domain.Entities.TransferApprovedItem>, 
    ITransferApprovedItemCommandRepository
{
    public TransferApprovedItemCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
