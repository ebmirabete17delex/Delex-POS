using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.ProductClass;

public class ProductClassCommandRepository : CommandHandlerBase<Domain.Entities.ProductClass>, IProductClassCommandRepository
{
    public ProductClassCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
