using Delex_POS.Application.Common.Interfaces.Repositories.Role;
namespace Delex_POS.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IRoleQueryRepository _roleQueryRepository;
    public CreateRoleCommandValidator(IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    
        RuleFor(v => v.Name)
            .NotEmpty()
            .MustAsync(NameNotExists);
        
        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(200);

    }
    private async Task<bool> NameNotExists(string name, CancellationToken cancellationToken)
    {
        var entity = await _roleQueryRepository.ExistAsync(e => e.Name == name, cancellationToken);
        return !entity;
    }
}

