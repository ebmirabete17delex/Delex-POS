using Delex_POS.Application.Common.Interfaces;
namespace Delex_POS.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IIdentityService _identityService;
    public CreateRoleCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
    
        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MustAsync(NameNotExists);
        
    }
    private async Task<bool> NameNotExists(string name, CancellationToken cancellationToken)
    {
        var entity = await _identityService.RoleExistAsync(name);
        return !entity;
    }
}

