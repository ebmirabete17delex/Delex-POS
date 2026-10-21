using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.Permission;

namespace Delex_POS.Application.AccessClaims.Commands.DeletePermission;

public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    private readonly IPermissionQueryRepository _permissionQueryRepository;
    public DeletePermissionCommandValidator(
        IAccessClaimQueryRepository accessClaimQueryRepository,
        IPermissionQueryRepository permissionQueryRepository)
    {
        _accessClaimQueryRepository = accessClaimQueryRepository;
        _permissionQueryRepository = permissionQueryRepository;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(PermissionExists);

        RuleFor(v => v.AccessId)
            .NotEmpty()
            .NotNull()
            .MustAsync(AccessClaimExists);
    }
    private async Task<bool> PermissionExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _permissionQueryRepository.ExistAsync(e => e.Id.Equals(id), cancellationToken);
        return entity;
    }
    private async Task<bool> AccessClaimExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _accessClaimQueryRepository.ExistAsync(e => e.Id.Equals(id), cancellationToken);
        return entity;
    }

}
