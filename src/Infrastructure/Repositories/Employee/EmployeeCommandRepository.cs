using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Employee;

public class EmployeeCommandRepository : CommandHandlerBase<Domain.Entities.Employee>, IEmployeeCommandRepository
{
    public EmployeeCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
