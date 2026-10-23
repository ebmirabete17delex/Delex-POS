using Delex_POS.Application.Common.Interfaces.Repositories.ReturntoSeller;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ReturnToSeller;

public class ReturnToSellerCommandRepository : 
    CommandHandlerBase<Domain.Entities.ReturnToSeller>, 
    IReturnToSellerCommandRepository
{
    public ReturnToSellerCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
