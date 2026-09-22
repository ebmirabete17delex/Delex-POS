using Delex_POS.Application.Common.Interfaces.Repositories.Role;
namespace Delex_POS.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    private readonly IRoleQueryRepository _RoleQueryRepository;
    public DeleteRoleCommandValidator(IRoleQueryRepository RoleQueryRepository)
    {
        _RoleQueryRepository = RoleQueryRepository;
    
        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(RoleExists);
    }

    private async Task<bool> RoleExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _RoleQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}


