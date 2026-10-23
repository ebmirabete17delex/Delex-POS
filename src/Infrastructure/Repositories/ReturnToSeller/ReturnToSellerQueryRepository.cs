using Delex_POS.Application.Common.Interfaces.Repositories.ReturntoSeller;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ReturnToSeller;

public class ReturnToSellerQueryRepository : 
    QueryHandlerBase<Domain.Entities.ReturnToSeller>, 
    IReturnToSellerQueryRepository
{
    public ReturnToSellerQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
