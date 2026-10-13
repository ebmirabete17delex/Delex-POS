using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    private readonly IIdentityService _identityService;
    public CreateEmployeeCommandValidator(
        IBranchQueryRepository branchQueryRepository,
        IIdentityService identityService)
    {
        _branchQueryRepository = branchQueryRepository;
        _identityService = identityService;

        RuleFor(v => v.LastName)
            .NotNull().WithMessage("Last Name is required.")
            .NotEmpty().WithMessage("Last Name is required.");

        RuleFor(v => v.FirstName)
            .NotNull().WithMessage("First Name is required.")
            .NotEmpty().WithMessage("First Name is required.");

        RuleFor(v => v.BirthDate)
            .NotNull().WithMessage("Date of birth is required.")
            .LessThanOrEqualTo(DateTime.Today.AddYears(-18)).WithMessage("You must be at least 18 years old.");

        RuleFor(v => v.BranchId)
            .NotEmpty().WithMessage("Branch is required.")
            .NotNull().WithMessage("Branch is required.")
            .MustAsync(BranchExist).WithMessage("A valid branch is required.");

        RuleFor(v => v.Gender)
            .NotNull().WithMessage("Gender is required.")
            .NotEmpty().WithMessage("Gender is required.")
            .IsInEnum().WithMessage("A valid gender is required.");

        RuleFor(v => v.CivilStatus)
            .NotNull().WithMessage("Civil Status is required.")
            .NotEmpty().WithMessage("Civil Status is required.")
            .IsInEnum().WithMessage("A valid civil status is required.");

        RuleFor(v => v.Email)
            .NotNull().WithMessage("Email is requied.")
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid Email address is required.");

        RuleFor(v => v.RoleId)
            .NotEmpty().WithMessage("Role is required.")
            .NotNull().WithMessage("Role is required.")
            .MustAsync(RoleExist).WithMessage("A valid branch is required.");
    }
    private async Task<bool> BranchExist(int id, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }

    private async Task<bool> RoleExist(string role, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetRoleByIdAsync(role, cancellationToken);
        return entity is not null;
    }

}
