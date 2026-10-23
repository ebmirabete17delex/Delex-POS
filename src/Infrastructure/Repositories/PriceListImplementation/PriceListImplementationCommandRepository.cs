using Delex_POS.Application.Common.Interfaces.Repositories.PriceListImplementation;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PriceListImplementation;

public class PriceListImplementationCommandRepository : 
    CommandHandlerBase<Domain.Entities.PriceListImplementation>, 
    IPriceListImplementationCommandRepository
{
    public PriceListImplementationCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
