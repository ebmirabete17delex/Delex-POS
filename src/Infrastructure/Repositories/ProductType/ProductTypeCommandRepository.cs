using Delex_POS.Application.Common.Interfaces.Repositories.ProductType;
using Delex_POS.Infrastructure.Data;
namespace Delex_POS.Infrastructure.Repositories.ProductType;

public class ProductTypeCommandRepository : CommandHandlerBase<Domain.Entities.ProductType>, IProductTypeCommandRepository
{
    public ProductTypeCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
