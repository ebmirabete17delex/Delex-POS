using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Generic;

public class GenericCommandRepository : CommandHandlerBase<Domain.Entities.Generic>, IGenericCommandRepository
{
    public GenericCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
