using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Application.Suppliers.Queries.SupplierDTOs;
namespace Delex_POS.Application.Suppliers.Queries.GetSupplier;

public record GetSupplierQuery(int Id) : IRequest<SupplierDto>;
public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, SupplierDto>
{
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    public GetSupplierQueryHandler(
        ISupplierQueryRepository supplierQueryRepository)
    {
        _supplierQueryRepository = supplierQueryRepository;
    }

    public async Task<SupplierDto> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        var entity = await _supplierQueryRepository
            .GetSupplier(request.Id)
            .FirstAsync(e => e.Id == request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return entity;
    }
}
