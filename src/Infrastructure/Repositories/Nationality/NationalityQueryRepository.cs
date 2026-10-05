using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Nationality;

public class NationalityQueryRepository : QueryHandlerBase<Domain.Entities.Nationality>, INationalityQueryRepository
{
    public NationalityQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
