using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;
using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Application.Roles.Queries.GetRolePermissions;

public record GetRolePermissionsQuery(string Id) : IRequest<List<RolePermissionDto>>;
public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, List<RolePermissionDto>>
{
    private readonly IRolePermissionQueryRepository _rolePermissionQueryRepository;
    public GetRolePermissionsQueryHandler(
        IRolePermissionQueryRepository rolePermissionQueryRepository)
    {
        _rolePermissionQueryRepository = rolePermissionQueryRepository;
    }
    public async Task<List<RolePermissionDto>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _rolePermissionQueryRepository.GetRolePermissionsAsync(request.Id, cancellationToken);
        return await entities.ToListAsync(cancellationToken);
    }
}
