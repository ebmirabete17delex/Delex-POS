using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ProductAttribute;

public class ProductAttributeQueryRepository : QueryHandlerBase<Domain.Entities.ProductAttribute>, IProductAttributeQueryRepository
{
    public ProductAttributeQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
