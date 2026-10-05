using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Application.Warehouses.Queries.WarehouseDTOs;

namespace Delex_POS.Application.Warehouses.Queries.GetWarehouse;

public record GetWarehouseQuery(int Id) : IRequest<WarehouseDto>;
public class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery,WarehouseDto>
{
    private readonly IMapper _mapper;
    private readonly IWarehouseQueryRepository _warehouseQueryRepository;
    public GetWarehouseQueryHandler(IMapper mapper, IWarehouseQueryRepository warehouseQueryRepository)
    {
        _mapper = mapper;
        _warehouseQueryRepository = warehouseQueryRepository;
    }
    public async Task<WarehouseDto> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
    {
        var entity = await _warehouseQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<WarehouseDto>(entity);
    }
}
