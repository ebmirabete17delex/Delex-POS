using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;

namespace Delex_POS.Application.Roles.Commands.DeleteRolePermission;

public record DeleteRolePermissionCommand : IRequest
{
    public string RoleId { get; set; } = string.Empty;
    public int PermissionId { get; set; }
};
public class DeleteRolePermissionCommandHandler : IRequestHandler<DeleteRolePermissionCommand>
{
    private readonly IRolePermissionCommandRepository _rolePermissionCommandRepository;
    private readonly IRolePermissionQueryRepository _rolePermissionQueryRepository;
    public DeleteRolePermissionCommandHandler(
        IRolePermissionQueryRepository rolePermissionQueryRepository,
        IRolePermissionCommandRepository rolePermissionCommandRepository)
    {
        _rolePermissionQueryRepository = rolePermissionQueryRepository;
        _rolePermissionCommandRepository = rolePermissionCommandRepository;
    }
    public async Task Handle(DeleteRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _rolePermissionQueryRepository.GetByRoleIdAndPermissionIdAsync(request.RoleId,request.PermissionId, cancellationToken);
        Guard.Against.NotFound(request.RoleId, entity);
        await _rolePermissionCommandRepository.DeleteAsync(entity.Id, cancellationToken);
    }
}
