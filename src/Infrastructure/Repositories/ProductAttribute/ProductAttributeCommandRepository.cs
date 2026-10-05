using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ProductAttribute;

public class ProductAttributeCommandRepository : CommandHandlerBase<Domain.Entities.ProductAttribute>, IProductAttributeCommandRepository
{
    public ProductAttributeCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
