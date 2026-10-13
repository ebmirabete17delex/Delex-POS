using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;

namespace Delex_POS.Application.Roles.Commands.UpdateRoleAccess;

public class UpdateRoleAccessCommandValidator : AbstractValidator<UpdateRoleAccessCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    private readonly IRoleAccessQueryRepository _roleAccessQueryRepository;
    public UpdateRoleAccessCommandValidator(
        IIdentityService identityService,
        IAccessClaimQueryRepository accessClaimQueryRepository,
        IRoleAccessQueryRepository roleAccessQueryRepository)
    {
        _identityService = identityService;
        _accessClaimQueryRepository = accessClaimQueryRepository;
        _roleAccessQueryRepository = roleAccessQueryRepository;

        RuleFor(v => v)
            .MustAsync(RoleAccessCombinationExist);
    }
    private async Task<bool> RoleAccessCombinationExist(UpdateRoleAccessCommand command, CancellationToken cancellationToken)
    {
        var exist = await _roleAccessQueryRepository
        .ExistAsync(e => e.RoleId == command.RoleId && e.AccessId == command.AccessId, cancellationToken);
        return exist;
    }

}
