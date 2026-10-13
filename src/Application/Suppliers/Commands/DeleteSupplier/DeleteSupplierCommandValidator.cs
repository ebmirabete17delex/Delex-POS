using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;

namespace Delex_POS.Application.Suppliers.Commands.DeleteSupplier;

public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
{
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    public DeleteSupplierCommandValidator(ISupplierQueryRepository supplierQueryRepository)
    {
        _supplierQueryRepository = supplierQueryRepository;

        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Customer Id is required.")
            .NotNull().WithMessage("Customer Id is required.")
            .MustAsync(SupplierExists).WithMessage("A valid customer Id is required.");

    }
    private async Task<bool> SupplierExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _supplierQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }

}
