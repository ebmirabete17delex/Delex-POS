using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.UserPermission;

public class UserPermissionQueryRepository : QueryHandlerBase<Domain.Entities.UserPermission>, IUserPermissionQueryRepository
{
    public UserPermissionQueryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<IQueryable<PermissionDto>> GetGrantedPermissionByUserId(string userId, CancellationToken cancellationToken)
    {
        string query = $"""
            SELECT distinct a.Feature, p.Name AS Action
            FROM AspNetUsers u
                INNER JOIN AspNetUserRoles ur
                    ON ur.UserId = u.Id
                INNER JOIN RoleAccesses ra 
                	ON ra.RoleId = ur.RoleId 
                	AND ra.IsDeleted = 0
                INNER JOIN Accesses a
                    ON ra.AccessId = a.Id
                    AND a.IsDeleted = 0
                INNER JOIN RolePermissions rp
                    ON ur.RoleId = rp.RoleId 
                    AND rp.IsDeleted = 0
                INNER JOIN Permissions p
                    ON rp.PermissionId = p.id
                    AND p.IsDeleted = 0
                WHERE u.Id = '{userId}'
                ORDER BY a.Feature 
            """;
        return _dbContext.Database.SqlQueryRaw<PermissionDto>(query);
    }

    public async Task<IQueryable<UserPermissionDto>> GetUserPermissionsAsync(int userId, CancellationToken cancellationToken)
    {
        string query = $"""
                SELECT up.Id, a.Feature, p.Name 
                FROM UserPermissions up
                INNER JOIN Permissions p
                ON up.PermissionId = p.Id
                INNER JOIN Accesses a
                ON a.Id = p.AccessId
                WHERE up.UserId = {userId} 
            """;
        return _dbContext.Database.SqlQueryRaw<UserPermissionDto>(query);
    }

    public async Task<IQueryable<UserPermissionDto>> GetAccessPermissionsAsync(int accessId, int userId, CancellationToken cancellationToken)
    {
        string query = $"""
                SELECT p.Id AS PermissionId
                    , a.Feature
                    , p.Name
                    , up.Id AS UserPermissionId
                FROM Permissions p
                INNER JOIN Accesses a
                ON p.accessId = a.Id
                LEFT OUTER JOIN UserPermissions up
                ON up.permissionId = p.Id
                AND up.userId = {userId}
                Where p.AccessId = {accessId}
            """;
        return _dbContext.Database.SqlQueryRaw<UserPermissionDto>(query);
    }
}
