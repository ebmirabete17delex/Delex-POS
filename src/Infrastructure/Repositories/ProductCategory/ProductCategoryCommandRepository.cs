using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ProductCategory;

public class ProductCategoryCommandRepository : CommandHandlerBase<Domain.Entities.ProductCategory>, IProductCategoryCommandRepository
{
    public ProductCategoryCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
