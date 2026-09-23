using Delex_POS.Application.Roles.Queries.RoleDTOs;
using Delex_POS.Application.Common.Enums;
namespace Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
public interface IRoleAccessQueryRepository : IQueryHandlerBase<Domain.Entities.RBAC.RoleAccess>
{
    // Task<Domain.Entities.RBAC.Role?> GetRoleByNameAsync(string name);
    // Task<Domain.Entities.Role?> GetRoleBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.Role?> GetRoleByNameAddressAsync(string name, Domain.Entities.RoleAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
    Task<Domain.Entities.RBAC.RoleAccess> GetByRoleIdAndAccessIdAsync(string roleId, int accessId);
    IQueryable<RoleAccessDto> GetByRoleId(string id, string sortBy, TableSort sortDirection);
}
