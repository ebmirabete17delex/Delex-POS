using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Infrastructure.Data;
namespace Delex_POS.Infrastructure.Repositories.Branch;
public class BranchCommandRepository : CommandHandlerBase<Domain.Entities.RBAC.Branch>, IBranchCommandRepository
{
    public BranchCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
