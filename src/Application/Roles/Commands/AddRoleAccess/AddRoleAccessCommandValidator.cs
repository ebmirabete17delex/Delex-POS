using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
namespace Delex_POS.Application.Roles.Commands.AddRoleAccess;

public class AddRoleAccessCommandValidator : AbstractValidator<AddRoleAccessCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    private readonly IRoleAccessQueryRepository _roleAccessQueryRepository;
    
    public AddRoleAccessCommandValidator(
        IIdentityService identityService, 
        IAccessClaimQueryRepository accessClaimQueryRepository,
        IRoleAccessQueryRepository roleAccessQueryRepository)
    {
        _identityService = identityService;
        _accessClaimQueryRepository = accessClaimQueryRepository;
        _roleAccessQueryRepository = roleAccessQueryRepository;
    
        RuleFor(v => v)
            .MustAsync(RoleAccessCombinationNotExist);

        RuleFor(v => v.RoleId)
            .NotEmpty()
            .MustAsync(RoleIdExist);

        RuleFor(v => v.AccessId)
            .NotEmpty()
            .MustAsync(AccessIdExist);
        
    }

    private async Task<bool> RoleAccessCombinationNotExist(AddRoleAccessCommand command, CancellationToken cancellationToken)
    {
        var exist = await _roleAccessQueryRepository
        .ExistAsync(e => e.RoleId == command.RoleId && e.AccessId == command.AccessId, cancellationToken);
        return !exist;
    }

    private async Task<bool> RoleIdExist(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetRoleByIdAsync(id, cancellationToken);
        return entity is not null;
    }

    private async Task<bool> AccessIdExist(int id, CancellationToken cancellationToken)
    {
        return await _accessClaimQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }

}