using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;
using Delex_POS.Application.Roles.Queries.RoleDTOs;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.RolePermission;

public class RolePermissionQueryRepository : QueryHandlerBase<Domain.Entities.RolePermission>, IRolePermissionQueryRepository
{
    public RolePermissionQueryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<Domain.Entities.RolePermission> GetByRoleIdAndPermissionIdAsync(
        string roleId, 
        int permissionId, 
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.RolePermissions
            .FirstOrDefaultAsync(e => 
                e.PermissionId == permissionId && 
                e.RoleId == roleId, cancellationToken);

        return entity!;
    }

    public async Task<IQueryable<RolePermissionDto>> GetRolePermissionsAsync(
        string roleId, 
        CancellationToken cancellationToken)
    {
        string query = $"""
                SELECT rp.Id, a.Feature, p.Name
                FROM RolePermissions rp
                INNER JOIN Permissions p
                ON rp.PermissionId = p.Id
                AND rp.RoleId = '{roleId}'
                AND p.IsDeleted = 0
                AND rp.IsDeleted = 0
                INNER JOIN Accesses a
                ON a.Id = p.AccessId
                AND a.IsDeleted = 0
            """;
        return _dbContext.Database.SqlQueryRaw<RolePermissionDto>(query);
    }

    public async Task<IQueryable<RolePermissionDto>> GetRoleAccessPermissionsAsync(
        int accessId, 
        string roleId, 
        CancellationToken cancellationToken)
    {
        string query = $"""
                SELECT p.Id AS PermissionId 
                    , a.Feature
                    , p.Name
                    , rp.Id AS RolePermissionId
                FROM Permissions p
                INNER JOIN Accesses a
                ON p.accessId = a.Id
                AND a.Id = {accessId}
                AND a.IsDeleted = 0
                AND p.IsDeleted = 0
                LEFT OUTER JOIN RolePermissions rp
                ON rp.PermissionId = p.Id
                AND rp.roleId = '{roleId}'
                AND rp.IsDeleted = 0            
            """;
        return _dbContext.Database.SqlQueryRaw<RolePermissionDto>(query);
    }
}
