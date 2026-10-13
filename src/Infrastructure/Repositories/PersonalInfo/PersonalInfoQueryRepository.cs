using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.PersonalInfo;

public class PersonalInfoQueryRepository : QueryHandlerBase<Domain.Entities.PersonalInfo>, IPersonalInfoQueryRepository
{
    public PersonalInfoQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
