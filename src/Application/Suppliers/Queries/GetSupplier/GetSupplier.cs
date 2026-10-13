using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Application.Suppliers.Queries.SupplierDTOs;
namespace Delex_POS.Application.Suppliers.Queries.GetSupplier;

public record GetSupplierQuery(int Id) : IRequest<SupplierDto>;
public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, SupplierDto>
{
    private readonly IMapper _mapper;
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    public GetSupplierQueryHandler(
        IMapper mapper,
        ISupplierQueryRepository supplierQueryRepository)
    {
        _mapper = mapper;
        _supplierQueryRepository = supplierQueryRepository;
    }

    public async Task<SupplierDto> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        var entity = await _supplierQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<SupplierDto>(entity);
    }
}
