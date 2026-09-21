namespace Delex_POS.Application.Common.Interfaces.Repositories.Branch;
public interface IBranchQueryRepository : IQueryHandlerBase<Domain.Entities.RBAC.Branch>
{
    // Task<Domain.Entities.RBAC.Branch?> GetBranchByNameAsync(string name);
    // Task<Domain.Entities.Branch?> GetBranchBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.Branch?> GetBranchByNameAddressAsync(string name, Domain.Entities.BranchAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
}
