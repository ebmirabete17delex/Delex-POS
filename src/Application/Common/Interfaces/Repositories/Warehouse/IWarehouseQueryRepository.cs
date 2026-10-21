using Delex_POS.Application.Warehouses.Queries.WarehouseDTOs;

namespace Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
public interface IWarehouseQueryRepository : IQueryHandlerBase<Domain.Entities.Warehouse>
{
    // Task<Domain.Entities.UserAccess?> GetUserAccessByNameAsync(string name);
    // Task<Domain.Entities.UserAccess?> GetUserAccessBySourceSystemIdAsync(string id, bool includeSupplyChain = false);
    // Task<Domain.Entities.UserAccess?> GetUserAccessByNameAddressAsync(string name, Domain.Entities.UserAccessAddress address);
    // Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId);
    // IQueryable<CompanyDto> GetCompanies();

    IQueryable<WarehouseDto> GetWarehouse(int id);
    // Task<bool> UserExistInNonDraftCompanies(int userId);
}
