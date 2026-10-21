using Delex_POS.Application.Branches.Queries.BranchDTOs;

namespace Delex_POS.Application.Common.Interfaces.Repositories.Branch;
public interface IBranchQueryRepository : IQueryHandlerBase<Domain.Entities.Branch>
{
    IQueryable<BranchDto> GetBranches();
    IQueryable<BranchDto> GetBranch(int id);
    // Task<Domain.Entities.Branch?> GetBranchByNameAsync(string name);
    // Task<Domain.Entities.Branch?> GetBranchBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.Branch?> GetBranchByNameAddressAsync(string name, Domain.Entities.BranchAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
}
