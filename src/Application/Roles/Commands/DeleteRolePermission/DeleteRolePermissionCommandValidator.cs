using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Permission;

namespace Delex_POS.Application.Roles.Commands.DeleteRolePermission;

public class DeleteRolePermissionCommandValidator : AbstractValidator<DeleteRolePermissionCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IPermissionQueryRepository _permissionQueryRepository;
    public DeleteRolePermissionCommandValidator(
        IIdentityService identityService, 
        IPermissionQueryRepository rolePermissionQueryRepository)
    {
        _identityService = identityService;
        _permissionQueryRepository = rolePermissionQueryRepository;

        RuleFor(v => v.RoleId)
            .NotNull()
            .NotEmpty()
            .MustAsync(RoleExist);

        RuleFor(v => v.PermissionId)
            .NotNull()
            .NotEmpty()
            .MustAsync(PermissionExist);

    }

    private async Task<bool> RoleExist(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetRoleByIdAsync(id, cancellationToken);
        return entity is not null;
    }

    private async Task<bool> PermissionExist(int id, CancellationToken cancellationToken)
    {
        return await _permissionQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }
}
