namespace Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;
public interface IProductPurchasingQueryRepository : 
    IQueryHandlerBase<Domain.Entities.ProductPurchasing>
{
    Task<Domain.Entities.ProductPurchasing> GetByProductIdAsync(
        int productId, CancellationToken cancellationToken = default);
}
