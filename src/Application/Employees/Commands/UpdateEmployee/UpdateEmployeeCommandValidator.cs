using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;

namespace Delex_POS.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;

    public UpdateEmployeeCommandValidator(
        IBranchQueryRepository branchQueryRepository,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;
        _employeeQueryRepository = employeeQueryRepository;

        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Employee Id is required.")
            .NotNull().WithMessage("Employee Id is required.")
            .MustAsync(EmployeeExists).WithMessage("A valid employee Id is required.");

        RuleFor(v => v.LastName)
            .NotNull().WithMessage("Last Name is required.")
            .NotEmpty().WithMessage("Last Name is required.");

        RuleFor(v => v.FirstName)
            .NotNull().WithMessage("First Name is required.")
            .NotEmpty().WithMessage("First Name is required.");

        RuleFor(v => v.BirthDate)
            .NotNull().WithMessage("Date of birth is required.")
            .LessThanOrEqualTo(DateTime.Today.AddYears(-18)).WithMessage("You must be at least 18 years old.");

        RuleFor(v => v.Gender)
            .NotNull().WithMessage("Gender is required.")
            .NotEmpty().WithMessage("Gender is required.")
            .IsInEnum().WithMessage("A valid gender is required.");

        RuleFor(v => v.CivilStatus)
            .NotNull().WithMessage("Civil Status is required.")
            .NotEmpty().WithMessage("Civil Status is required.")
            .IsInEnum().WithMessage("A valid civil status is required.");

        RuleFor(v => v.TIN)
            .MaximumLength(12).WithMessage("Invalid Tax identification number.")
            .MinimumLength(9).WithMessage("Invalid Tax identification number.");
    }
    private async Task<bool> EmployeeExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _employeeQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
