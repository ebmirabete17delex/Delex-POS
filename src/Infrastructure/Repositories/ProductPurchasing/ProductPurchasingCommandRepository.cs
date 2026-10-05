using Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ProductPurchasing;

public class ProductPurchasingCommandRepository : CommandHandlerBase<Domain.Entities.ProductPurchasing>, IProductPurchasingCommandRepository
{
    public ProductPurchasingCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
