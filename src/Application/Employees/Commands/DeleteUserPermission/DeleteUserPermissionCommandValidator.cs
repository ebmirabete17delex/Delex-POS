using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;
using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;

namespace Delex_POS.Application.Employees.Commands.DeleteUserPermission;

public class DeleteUserPermissionCommandValidator : AbstractValidator<DeleteUserPermissionCommand>
{
    private readonly IUserPermissionQueryRepository _userPermissionQueryRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public DeleteUserPermissionCommandValidator(
        IUserPermissionQueryRepository userPermissionQueryRepository,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
        _userPermissionQueryRepository = userPermissionQueryRepository;

        RuleFor(v => v.UserId)
            .NotNull()
            .NotEmpty()
            .MustAsync(UserExist);

        RuleFor(v => v.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(UserPermissionExist);

    }
    private async Task<bool> UserExist(int id, CancellationToken cancellationToken)
    {
        var entity = await _employeeQueryRepository.GetByIdAsync(id, cancellationToken);
        return entity is not null;
    }

    private async Task<bool> UserPermissionExist(int id, CancellationToken cancellationToken)
    {
        return await _userPermissionQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }
}
