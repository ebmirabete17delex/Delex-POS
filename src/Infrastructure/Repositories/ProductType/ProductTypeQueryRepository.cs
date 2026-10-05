using Delex_POS.Application.Common.Interfaces.Repositories.ProductType;
using Delex_POS.Infrastructure.Data;
namespace Delex_POS.Infrastructure.Repositories.ProductType;

public class ProductTypeQueryRepository : QueryHandlerBase<Domain.Entities.ProductType>, IProductTypeQueryRepository
{
    public ProductTypeQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
