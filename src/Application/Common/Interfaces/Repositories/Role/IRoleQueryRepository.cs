namespace Delex_POS.Application.Common.Interfaces.Repositories.Role;
public interface IRoleQueryRepository : IQueryHandlerBase<Domain.Entities.RBAC.Role>
{
    // Task<Domain.Entities.RBAC.Role?> GetRoleByNameAsync(string name);
    // Task<Domain.Entities.Role?> GetRoleBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.Role?> GetRoleByNameAddressAsync(string name, Domain.Entities.RoleAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
}
