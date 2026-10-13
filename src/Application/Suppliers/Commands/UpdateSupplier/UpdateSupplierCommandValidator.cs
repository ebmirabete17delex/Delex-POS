using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
namespace Delex_POS.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
{
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    private readonly IBranchQueryRepository _branchQueryRepository;
    public UpdateSupplierCommandValidator(
        ISupplierQueryRepository supplierQueryRepository,
        IBranchQueryRepository branchQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;
        _supplierQueryRepository = supplierQueryRepository;

        RuleFor(v => v.Id)
        .NotEmpty().WithMessage("Customer Id is required.")
        .NotNull().WithMessage("Customer Id is required.")
        .MustAsync(SupplierExists).WithMessage("A valid customer Id is required.");

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

        RuleFor(v => v.Email)
            .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(v => v.TIN)
            .MaximumLength(12).WithMessage("Invalid Tax identification number.")
            .MinimumLength(9).WithMessage("Invalid Tax identification number.");
    }
    private async Task<bool> SupplierExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _supplierQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
    private async Task<bool> BranchExist(int id, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
