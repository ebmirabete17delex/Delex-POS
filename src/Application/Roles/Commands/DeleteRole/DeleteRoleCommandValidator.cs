using Delex_POS.Application.Common.Interfaces;
namespace Delex_POS.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    private readonly IIdentityService _identityService;
    public DeleteRoleCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
    
        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(RoleExists);
    }

    private async Task<bool> RoleExists(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.RoleExistAsync(id);
        return entity;
    }
}


