using Delex_POS.Application.Common.Interfaces.Repositories.TransferRequest;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TransferRequest;

public class TransferRequestQueryRepository : 
    QueryHandlerBase<Domain.Entities.TransferRequest>, 
    ITransferRequestQueryRepository
{
    public TransferRequestQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
