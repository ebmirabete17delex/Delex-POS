using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;

namespace Delex_POS.Application.Suppliers.Commands.DeleteSupplier;

public record DeleteSupplierCommand(int Id) : IRequest;
public class DeleteSupplierCommandHandler : IRequest<DeleteSupplierCommand>
{
    private readonly ISupplierCommandRepository _supplierCommandRepository;
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    public DeleteSupplierCommandHandler(
        ISupplierQueryRepository supplierQueryRepository,
        ISupplierCommandRepository supplierCommandRepository)
    {
        _supplierQueryRepository = supplierQueryRepository;
        _supplierCommandRepository = supplierCommandRepository;
    }

    public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var entity = await _supplierQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _supplierCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
