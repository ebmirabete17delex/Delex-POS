using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Nationality;

public class NationalityCommandRepository : CommandHandlerBase<Domain.Entities.Nationality>, INationalityCommandRepository
{
    public NationalityCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
