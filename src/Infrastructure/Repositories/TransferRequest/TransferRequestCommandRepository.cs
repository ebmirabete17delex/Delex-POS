using Delex_POS.Application.Common.Interfaces.Repositories.TransferRequest;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TransferRequest;

public class TransferRequestCommandRepository : 
    CommandHandlerBase<Domain.Entities.TransferRequest>, 
    ITransferRequestCommandRepository
{
    public TransferRequestCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
