using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ProductCategory;

public class ProductCategoryQueryRepository : QueryHandlerBase<Domain.Entities.ProductCategory>, IProductCategoryQueryRepository
{
    public ProductCategoryQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
