using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;

namespace Delex_POS.Application.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public DeleteEmployeeCommandValidator(IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Employee Id is required.")
            .NotNull().WithMessage("Employee Id is required.")
            .MustAsync(EmployeeExists).WithMessage("A valid employee Id is required.");
    }
    private async Task<bool> EmployeeExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _employeeQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
