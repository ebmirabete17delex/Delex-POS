using Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.ProductPurchasing;

public class ProductPurchasingQueryRepository : 
    QueryHandlerBase<Domain.Entities.ProductPurchasing>, 
    IProductPurchasingQueryRepository
{
    public ProductPurchasingQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    public async Task<Domain.Entities.ProductPurchasing> GetByProductIdAsync(
        int productId, 
        CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.ProductPurchasings
            .FirstOrDefaultAsync(e => e.ProductId == productId, cancellationToken);
        Guard.Against.NotFound(productId, entity);
        return entity!;
    }
}
