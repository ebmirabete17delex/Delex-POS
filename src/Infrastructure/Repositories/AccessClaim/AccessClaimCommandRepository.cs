using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Infrastructure.Data;
namespace Delex_POS.Infrastructure.Repositories.AccessClaim;
public class AccessClaimCommandRepository : CommandHandlerBase<Domain.Entities.RBAC.AccessClaim>, IAccessClaimCommandRepository
{
    public AccessClaimCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
