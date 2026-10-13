using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Customer;

public class CustomerCommandRepository : CommandHandlerBase<Domain.Entities.Customer>, ICustomerCommandRepository
{
    public CustomerCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
