using Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ProductSale;

public class ProductSaleCommandRepository : CommandHandlerBase<Domain.Entities.ProductSale>, IProductSaleCommandRepository
{
    public ProductSaleCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
