using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.Permission;

namespace Delex_POS.Application.Employees.Commands.CreateUserPermission;
public class CreateUserPermissionCommandValidator : AbstractValidator<CreateUserPermissionCommand>
{
    private readonly IPermissionQueryRepository _permissionQueryRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public CreateUserPermissionCommandValidator(
        IPermissionQueryRepository permissionQueryRepository, 
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
        _permissionQueryRepository = permissionQueryRepository;

        RuleFor(v => v.UserId)
            .NotNull()
            .NotEmpty()
            .MustAsync(UserExist);

        RuleFor(v => v.PermissionId)
            .NotNull()
            .NotEmpty()
            .MustAsync(PermissionExist);
    }
    private async Task<bool> UserExist(int id, CancellationToken cancellationToken)
    {
        var entity = await _employeeQueryRepository.GetByIdAsync(id, cancellationToken);
        return entity is not null;
    }

    private async Task<bool> PermissionExist(int id, CancellationToken cancellationToken)
    {
        return await _permissionQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }

}
