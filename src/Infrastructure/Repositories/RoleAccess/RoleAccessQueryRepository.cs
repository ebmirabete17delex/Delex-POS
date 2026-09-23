using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
using Delex_POS.Infrastructure.Data;
using Delex_POS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Infrastructure.Repositories.RoleAccess;

public class RoleAccessQueryRepository : QueryHandlerBase<Domain.Entities.RBAC.RoleAccess>, IRoleAccessQueryRepository
{
    public RoleAccessQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<Domain.Entities.RBAC.RoleAccess> GetByRoleIdAndAccessIdAsync(string roleId, int accessId)
    {
        var entity = await _dbContext.RoleAccesses.FirstOrDefaultAsync(e => e.RoleId == roleId && e.AccessId == accessId);
        Guard.Against.NotFound(roleId, entity);
        return entity!;
    }

    public IQueryable<RoleAccessDto> GetByRoleId(string id, string sortBy, TableSort sortDirection)
    {
        string query = $"""
                        SELECT r.Id as [Id]
                                , r.RoleId
                                , r.AccessId
                              	, a.Name
                              	, a.Feature
                              	, a.BackendUrl
                                , a.FrontendUrl
                        FROM RoleAccesses r
                        		INNER JOIN Accesses a
                        					ON r.accessid = a.id
                        WHERE r.RoleId = {id}
                        """;
        return _dbContext.Database.SqlQueryRaw<RoleAccessDto>(query).OrderBy(sortBy, sortDirection);
    }

    // public async Task<Domain.Entities.RBAC.Role?> GetRoleByNameAsync(string name)
    // {
    //     return await _dbContext.Branches.FirstOrDefaultAsync(x => x.Name == name);
    // }

    // public async Task<Domain.Entities.RBAC.Branch?> GetBranchBySourceSystemIdAsync(string id,
    //     bool includeSupplyChain = false)
    // {
    //     return includeSupplyChain
    //         ? await _dbContext.Branches
    //             .Include(x => x.SupplyChainParents)
    //             .Include(x => x.CompanyAddresses)
    //             .ThenInclude(x => x.Country)
    //             .FromCacheFirstAsync(x => x.SourceSystemId == id, id)
    //         : await _dbContext.Branches
    //             .Include(x => x.CompanyAddresses)
    //             .ThenInclude(x => x.Country)
    //             .FromCacheFirstAsync(x => x.SourceSystemId == id, id);
    // }

    // public async Task<Domain.Entities.RBAC.Branch?> GetBranchByNameAddressAsync(string name,
    //     Domain.Entities.RBAC.BranchAddress address)
    // {

    //     string addr = address!.CountryId.ToString().ToLower().Trim()
    //                   + address!.PostalCode.ToLower().Trim()
    //                   + address!.StateProvinceRegion.ToLower().Trim()
    //                   + address!.City.ToLower().Trim()
    //                   + address!.Address1.ToLower().Trim()
    //                   + address!.Address2.ToLower().Trim();

    //     return await _dbContext.Branches
    //         .Include(c => c.CompanyAddresses)
    //         .Include(a => a.SupplyChainParents)
    //         .ThenInclude(b => b.SupplyChainUsers)
    //         .FirstOrDefaultAsync(
    //             x => x.Name == name
    //                  && x.CompanyAddresses.Any(
    //                      a => a.CountryId.ToString().ToLower().Trim() +
    //                          a.PostalCode.ToLower().Trim() +
    //                          a.StateProvinceRegion.ToLower().Trim() +
    //                          a.City.ToLower().Trim() +
    //                          a.Address1.ToLower().Trim() +
    //                          a.Address2.ToLower().Trim() == addr)
    //         );
    // }

    // public async Task<bool> UserExistInNonDraftCompanies(int userId)
    // {
    //     return await _dbContext.SupplyChainUsers
    //         .Include(x => x.SupplyChain)
    //         .ThenInclude(x => x!.Company)
    //         .AnyAsync(x => x.UserId == userId && x.SupplyChain!.Status != CompanyStatus.Draft);
    // }

    // public IQueryable<CompanyDto> GetCompanies()
    // {
    //     string query = $"""
    //                     SELECT c.SourceSystemId as [Id]
    //                           	, c.[Name] AS [Company]
    //                           	, RTRIM(ca.Address1 + ' '
    //                                 		+ IIF(ca.Address2 = 'N/A', '', ca.Address2 + ' ')
    //                                 		+ IIF(ca.City = 'N/A', '', ca.City + ' ')
    //                                 		+ IIF(ca.StateProvinceRegion = 'N/A', '', ca.StateProvinceRegion + ' ')
    //                                 		+ IIF(ca.PostalCode = 'N/A', '', ca.PostalCode)) AS [Address]
    //                           	, cs.[Name] AS [Country]
    //                           	, cs.[ImageUrl] AS [CountryImage]
    //                             , ISNULL(u.FirstName, '') as [FirstName]
    //                             , ISNULL(u.LastName, '') as [LastName]
    //                           	, ISNULL(u.Email, '') AS [Email]
    //                           	, IIF(u.Phone = '', '', ISNULL('(' + cs.DialCode + ') ' + u.Phone, '')) AS [Phone]
    //                             , CASE sc.Status
    //                                       	WHEN 1 THEN 'Invited'
    //                                       	WHEN 2 THEN 'Active'
    //                                       	WHEN 3 THEN 'Draft'
    //                                       	WHEN 4 THEN 'Pending Review'
    //                                       	ELSE 'Unknown'
    //                           	  END AS [Status]
    //                           	, c.Created AS [AddedOn]
    //                     FROM Companies c
    //                     		INNER JOIN SupplyChain sc
    //                     					ON c.id = sc.CompanyId
    //                           	LEFT JOIN SupplyChainUsers cu
    //                                 		ON sc.Id = cu.SupplyChainId
    //                             LEFT JOIN Users u
    //                                 		ON cu.UserId = u.Id
    //                           	INNER JOIN CompanyAddresses ca
    //                                 		ON c.Id = ca.CompanyId
    //                           	INNER JOIN Countries cs
    //                                 		ON ca.CountryId = cs.Id
    //                     WHERE sc.SupplyChainRoleId = 1
    //                     	AND (cu.AccessRoleId = 5 OR cu.Id IS NULL)
    //                     """;

    //     return _dbContext.Database.SqlQueryRaw<CompanyDto>(query);
    // }

    // public async Task<Domain.Entities.Company?> GetCompanyByQROnboadingDisplayId(Guid displayId)
    // {
    //     return await _dbContext.Companies
    //         .Where(c => c.QROnboardingCodes.Any(q => q.DisplayId == displayId))
    //         .Include(c => c.QROnboardingCodes.Where(q => q.DisplayId == displayId))
    //         .FirstOrDefaultAsync(x => x.QROnboardingCodes.Any());
    // }
}
