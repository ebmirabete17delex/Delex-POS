using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ProductClass;

public class ProductClassQueryRepository : QueryHandlerBase<Domain.Entities.ProductClass>, IProductClassQueryRepository
{
    public ProductClassQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
