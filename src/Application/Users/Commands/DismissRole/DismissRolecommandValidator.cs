using Delex_POS.Application.Common.Interfaces;
namespace Delex_POS.Application.Users.Commands.DismissRole;

public class DismissRoleCommandValidator : AbstractValidator<DismissRoleCommand>
{
    private readonly IIdentityService _identityService;
    
    public DismissRoleCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
    
        RuleFor(v => v)
            .MustAsync(UserAccessCombinationExist);

        RuleFor(v => v.UserId)
            .NotEmpty()
            .MustAsync(UserIdExist);

        RuleFor(v => v.Role)
            .NotEmpty()
            .MustAsync(RoleIdExist);
        
    }

    private async Task<bool> UserAccessCombinationExist(DismissRoleCommand command, CancellationToken cancellationToken)
    {
        var exist = await _identityService.IsInRoleAsync(command.UserId, command.Role);
        return exist;
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