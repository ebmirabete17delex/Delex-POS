using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;
using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Application.Roles.Queries.GetRoleAccessPermissions;
public record GetRoleAccessPermissionsQuery : IRequest<List<RoleAccessPermissionDto>>
{
    public string RoleId { get; set; } = string.Empty;
}
public class GetRoleAccessPermissionsQueryHandler : IRequestHandler<GetRoleAccessPermissionsQuery,List<RoleAccessPermissionDto>>
{
    private readonly IRolePermissionQueryRepository _rolePermissionQueryRepository;
    private readonly IRoleAccessQueryRepository _roleAccessQueryRepository;
    public GetRoleAccessPermissionsQueryHandler(
        IRoleAccessQueryRepository roleAccessQueryRepository,
        IRolePermissionQueryRepository rolePermissionQueryRepository)
    {
        _roleAccessQueryRepository = roleAccessQueryRepository;
        _rolePermissionQueryRepository = rolePermissionQueryRepository;
    }

    public async Task<List<RoleAccessPermissionDto>> Handle(
        GetRoleAccessPermissionsQuery request, 
        CancellationToken cancellationToken)
    {
        var accessList = new List<RoleAccessPermissionDto>();
        var accesses = await _roleAccessQueryRepository.GetByRoleId(request.RoleId).ToListAsync(cancellationToken);
        Guard.Against.NotFound(request.RoleId, accesses);
        foreach (var a in accesses)
        {
            var roleAccess = new RoleAccessPermissionDto
            {
                AccessId = a.AccessId, 
                Name = a.Name, 
                Feature = "",
                Permissions = []
            };
            var entities = await _rolePermissionQueryRepository
                .GetRoleAccessPermissionsAsync(a.AccessId, request.RoleId, cancellationToken);            
            if (entities is not null)
            {
                var permissions = await entities.ToListAsync(cancellationToken);
                foreach (var permission in permissions)
                {
                    roleAccess.Permissions.Add(new RolePermissionDto
                    {
                        RolePermissionId = permission.RolePermissionId,
                        PermissionId = permission.PermissionId,
                        Name = permission.Name,
                        Feature = permission.Feature
                    });
                }
            }
            accessList.Add(roleAccess);
        }
        return accessList;
    }
}
