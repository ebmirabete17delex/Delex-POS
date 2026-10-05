using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.CustomerType;

public class CustomerTypeCommandRepository : CommandHandlerBase<Domain.Entities.CustomerType>, ICustomerTypeCommandRepository
{
    public CustomerTypeCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
