using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Generic;

public class GenericQueryRepository : QueryHandlerBase<Domain.Entities.Generic>, IGenericQueryRepository
{
    public GenericQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
