using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Product;

public class ProductCommandRepository : CommandHandlerBase<Domain.Entities.Product>, IProductCommandRepository
{
    public ProductCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
