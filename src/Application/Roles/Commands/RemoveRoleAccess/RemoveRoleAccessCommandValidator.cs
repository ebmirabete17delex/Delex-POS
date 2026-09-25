using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
namespace Delex_POS.Application.Roles.Commands.RemoveRoleAccess;

public class RemoveRoleAccessCommandValidator : AbstractValidator<RemoveRoleAccessCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    private readonly IRoleAccessQueryRepository _roleAccessQueryRepository;
    
    public RemoveRoleAccessCommandValidator(
        IIdentityService identityService, 
        IAccessClaimQueryRepository accessClaimQueryRepository,
        IRoleAccessQueryRepository roleAccessQueryRepository)
    {
        _identityService = identityService;
        _accessClaimQueryRepository = accessClaimQueryRepository;
        _roleAccessQueryRepository = roleAccessQueryRepository;
    
        RuleFor(v => v)
            .NotEmpty()
            .NotNull()
            .MustAsync(RoleAccessCombinationExist);
    }

    private async Task<bool> RoleAccessCombinationExist(RemoveRoleAccessCommand command, CancellationToken cancellationToken)
    {
        var exist = await _roleAccessQueryRepository
        .ExistAsync(e => e.RoleId == command.RoleId && e.AccessId == command.AccessId, cancellationToken);
        return exist;
    }
}