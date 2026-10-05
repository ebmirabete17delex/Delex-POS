using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Product;

public class ProductQueryRepository : QueryHandlerBase<Domain.Entities.Product>, IProductQueryRepository
{
    public ProductQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
