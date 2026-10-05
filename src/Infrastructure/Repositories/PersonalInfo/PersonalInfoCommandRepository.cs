using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.PersonalInfo;

public class PersonalInfoCommandRepository : CommandHandlerBase<Domain.Entities.PersonalInfo>, IPersonalInfoCommandRepository
{
    public PersonalInfoCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
