using Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.ProductSale;

public class ProductSaleQueryRepository : 
    QueryHandlerBase<Domain.Entities.ProductSale>, 
    IProductSaleQueryRepository
{
    public ProductSaleQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    public async Task<Domain.Entities.ProductSale> GetByProductIdAsync(
        int productId, 
        CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.ProductSales
            .FirstOrDefaultAsync(ps => ps.ProductId == productId, cancellationToken);
        Guard.Against.NotFound(productId, entity);
        return entity!;
    }
}
