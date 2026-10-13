using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Suppliers.Commands.CreateSupplier;

public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    public CreateSupplierCommandValidator(
        IBranchQueryRepository branchQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;

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
            .IsInEnum().WithMessage("A valid gender is required.");

        RuleFor(v => v.CivilStatus)
            .IsInEnum().WithMessage("A valid civil status is required.");
    }

    private async Task<bool> BranchExist(int id, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
