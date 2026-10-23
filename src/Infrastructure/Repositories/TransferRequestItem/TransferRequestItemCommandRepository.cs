using Delex_POS.Application.Common.Interfaces.Repositories.TransferRequestItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TransferRequestItem;

public class TransferRequestItemCommandRepository : CommandHandlerBase<Domain.Entities.TransferRequestItem>, ITransferRequestItemCommandRepository
{
    public TransferRequestItemCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
