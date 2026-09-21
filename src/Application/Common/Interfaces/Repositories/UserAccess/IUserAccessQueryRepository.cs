namespace Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
public interface IUserAccessQueryRepository : IQueryHandlerBase<Domain.Entities.RBAC.UserAccess>
{
    // Task<Domain.Entities.RBAC.UserAccess?> GetUserAccessByNameAsync(string name);
    // Task<Domain.Entities.UserAccess?> GetUserAccessBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.UserAccess?> GetUserAccessByNameAddressAsync(string name, Domain.Entities.UserAccessAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
}
