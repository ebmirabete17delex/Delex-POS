namespace Delex_POS.Application.Common.Interfaces.Repositories.UserRole;
public interface IUserRoleQueryRepository : IQueryHandlerBase<Domain.Entities.RBAC.UserRole>
{
    // Task<Domain.Entities.RBAC.UserRole?> GetUserRoleByNameAsync(string name);
    // Task<Domain.Entities.UserRole?> GetUserRoleBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.UserRole?> GetUserRoleByNameAddressAsync(string name, Domain.Entities.UserRoleAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
}
