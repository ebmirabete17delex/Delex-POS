namespace Delex_POS.Application.Common.Interfaces.Repositories.Account;
public interface IAccountQueryRepository : IQueryHandlerBase<Domain.Entities.Account>
{
    // Task<Domain.Entities.AccessClaim?> GetAccessClaimByNameAsync(string name);
    // Task<Domain.Entities.AccessClaim?> GetAccessClaimBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.AccessClaim?> GetAccessClaimByNameAddressAsync(string name, Domain.Entities.AccessClaimAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
}
