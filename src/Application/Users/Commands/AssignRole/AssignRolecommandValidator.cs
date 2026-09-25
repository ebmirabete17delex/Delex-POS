using Delex_POS.Application.Common.Interfaces;
namespace Delex_POS.Application.Users.Commands.AssignRole;

public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
{
    private readonly IIdentityService _identityService;
    
    public AssignRoleCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
    
        RuleFor(v => v)
            .MustAsync(UserAccessCombinationNotExist);

        RuleFor(v => v.UserId)
            .NotEmpty()
            .NotNull()
            .MustAsync(UserIdExist);

        RuleFor(v => v.Role)
            .NotEmpty()
            .NotNull()
            .MustAsync(RoleIdExist);
    }

    private async Task<bool> UserAccessCombinationNotExist(AssignRoleCommand command, CancellationToken cancellationToken)
    {
        var exist = await _identityService.IsInRoleAsync(command.UserId, command.Role);
        return !exist;
    }

    private async Task<bool> UserIdExist(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetUserByIdAsync(id);
        return entity is not null;
    }

    private async Task<bool> RoleIdExist(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetRoleByIdAsync(id, cancellationToken);
        return entity is not null;
    }
}