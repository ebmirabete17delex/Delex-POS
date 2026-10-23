using Delex_POS.Application.Common.Interfaces.Repositories.TransferRequestItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TransferRequestItem;

public class TransferRequestItemQueryRepository : QueryHandlerBase<Domain.Entities.TransferRequestItem>, ITransferRequestItemQueryRepository
{
    public TransferRequestItemQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
