using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.CustomerType;

public class CustomerTypeQueryRepository : QueryHandlerBase<Domain.Entities.CustomerType>, ICustomerTypeQueryRepository
{
    public CustomerTypeQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
