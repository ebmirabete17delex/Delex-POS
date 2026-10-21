using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.UserPermission;

public class UserPermissionQueryRepository : QueryHandlerBase<Domain.Entities.UserPermission>, IUserPermissionQueryRepository
{
    public UserPermissionQueryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

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
