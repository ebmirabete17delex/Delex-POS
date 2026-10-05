namespace Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;
public interface IProductSaleQueryRepository : 
    IQueryHandlerBase<Domain.Entities.ProductSale>
{
    Task<Domain.Entities.ProductSale> GetByProductIdAsync(
        int productId, CancellationToken cancellationToken = default);
}
