using Delex_POS.Application.Common.Interfaces.Repositories.TransferApprovedItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TransferApprovedItem;

public class TransferApprovedItemQueryRepository : 
    QueryHandlerBase<Domain.Entities.TransferApprovedItem>, 
    ITransferApprovedItemQueryRepository
{
    public TransferApprovedItemQueryRepository(ApplicationDbContext dbcontext) : base(dbcontext) { }
}
