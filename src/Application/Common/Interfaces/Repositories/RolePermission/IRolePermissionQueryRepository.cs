using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;

public interface IRolePermissionQueryRepository : IQueryHandlerBase<Domain.Entities.RolePermission>
{
    Task<Domain.Entities.RolePermission> GetByRoleIdAndPermissionIdAsync(
            string roleId,
            int permissionId,
            CancellationToken cancellationToken);
    Task<IQueryable<RolePermissionDto>> GetRolePermissionsAsync(
        string RoleId, 
        CancellationToken cancellationToken);
    Task<IQueryable<RolePermissionDto>> GetRoleAccessPermissionsAsync(
        int accessId, 
        string roleId, 
        CancellationToken cancellationToken);
}
