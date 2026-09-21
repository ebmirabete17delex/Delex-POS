namespace Delex_POS.Application.Common.Interfaces.Repositories.User;
public interface IUserQueryRepository : IQueryHandlerBase<Domain.Entities.RBAC.User>
{
    // Task<Domain.Entities.RBAC.User?> GetUserByNameAsync(string name);
    // Task<Domain.Entities.User?> GetUserBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.User?> GetUserByNameAddressAsync(string name, Domain.Entities.UserAddress address);
    // Task<Domain.Entities.User?> GetUserByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    // Task<bool> UserExistInNonDraftCompanies(int userId);
}
