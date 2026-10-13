using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Supplier;

public class SupplierCommandRepository : CommandHandlerBase<Domain.Entities.Supplier>, ISupplierCommandRepository
{
    public SupplierCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
