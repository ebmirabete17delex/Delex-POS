using Delex_POS.Application.Common.Interfaces.Repositories.ReturnToSellerItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ReturnToSellerItem;

public class ReturnToSellerItemCommandRepository : CommandHandlerBase<Domain.Entities.ReturnToSellerItem>, IReturnToSellerItemCommandRepository
{
    public ReturnToSellerItemCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
