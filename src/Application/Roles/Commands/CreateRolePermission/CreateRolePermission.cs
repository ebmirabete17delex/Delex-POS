using System.Runtime.CompilerServices;
using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;

namespace Delex_POS.Application.Roles.Commands.CreateRolePermission;
public record CreateRolePermissionCommand : IRequest<int>
{
    public string RoleId { get; set; } = string.Empty;
    public int PermissionId { get; set; }
}
public class CreateRolePermissionCommandHandler : IRequestHandler<CreateRolePermissionCommand, int>
{
    private readonly IRolePermissionCommandRepository _rolePermissionCommandRepository;
    private readonly IRolePermissionQueryRepository _rolePermissionQueryRepository;
    public CreateRolePermissionCommandHandler(
        IRolePermissionCommandRepository rolePermissionCommandRepository, 
        IRolePermissionQueryRepository rolePermissionQueryRepository)
    {
        _rolePermissionQueryRepository = rolePermissionQueryRepository;
        _rolePermissionCommandRepository = rolePermissionCommandRepository;
    }

    public async Task<int> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _rolePermissionQueryRepository.GetAll().SingleOrDefaultAsync(e => e.RoleId == request.RoleId && e.PermissionId == request.PermissionId, cancellationToken);
        if (entity is null)
            return await _rolePermissionCommandRepository.AddAsync(new Domain.Entities.RolePermission(request.RoleId, request.PermissionId), cancellationToken);
        else
            return entity.Id;

    }
}
