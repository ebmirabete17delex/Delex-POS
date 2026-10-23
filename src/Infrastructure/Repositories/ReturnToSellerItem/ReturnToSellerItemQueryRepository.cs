using Delex_POS.Application.Common.Interfaces.Repositories.ReturnToSellerItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ReturnToSellerItem;

public class ReturnToSellerItemQueryRepository : QueryHandlerBase<Domain.Entities.ReturnToSellerItem>, IReturnToSellerItemQueryRepository
{
    public ReturnToSellerItemQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
