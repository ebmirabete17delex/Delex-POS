using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
namespace Delex_POS.Application.Employees.Commands.AssignRole;

public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    
    public AssignRoleCommandValidator(
        IIdentityService identityService,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _identityService = identityService;
        _employeeQueryRepository = employeeQueryRepository;
    
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
        var ee = await _employeeQueryRepository.GetByIdAsync(command.UserId, cancellationToken);
        Guard.Against.NotFound(command.UserId, ee);

        var exist = await _identityService.IsInRoleAsync(ee.UserId, command.Role);
        return !exist;
    }

    private async Task<bool> UserIdExist(int id, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(id, cancellationToken);
        Guard.Against.NotFound(id, ee);

        var entity = await _identityService.GetUserByIdAsync(ee.UserId);
        return entity is not null;
    }

    private async Task<bool> RoleIdExist(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetRoleByIdAsync(id, cancellationToken);
        return entity is not null;
    }
}
