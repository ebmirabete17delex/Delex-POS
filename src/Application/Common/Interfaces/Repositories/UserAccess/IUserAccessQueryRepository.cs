using Delex_POS.Application.Users.Queries.UserDTOs;
using Delex_POS.Application.Common.Enums;
namespace Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
public interface IUserAccessQueryRepository : IQueryHandlerBase<Domain.Entities.RBAC.UserAccess>
{
    // Task<Domain.Entities.RBAC.UserAccess?> GetUserAccessByNameAsync(string name);
    // Task<Domain.Entities.UserAccess?> GetUserAccessBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.UserAccess?> GetUserAccessByNameAddressAsync(string name, Domain.Entities.UserAccessAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
    Task<Domain.Entities.RBAC.UserAccess> GetByUserIdAndAccessIdAsync(string userId, int accessId);
    IQueryable<UserAccessDto> GetByUserId(string id, string sortBy, TableSort sortDirection);
}