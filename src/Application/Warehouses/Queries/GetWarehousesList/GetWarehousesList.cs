using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Warehouses.Queries.WarehouseDTOs;

namespace Delex_POS.Application.Warehouses.Queries.GetWarehousesList;

public record GetWarehousesListQuery : IRequest<List<WarehouseListDto>>;
public class GetWarehousesListQueryHandler : IRequestHandler<GetWarehousesListQuery, List<WarehouseListDto>>
{
    private readonly IWarehouseQueryRepository _query;
    private readonly IMapper _mapper;
    public GetWarehousesListQueryHandler(IMapper mapper, IWarehouseQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<WarehouseListDto>> Handle(GetWarehousesListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll()
            .Where(w => w.IsActive == true)
            .ProjectToListAsync<WarehouseListDto>(_mapper.ConfigurationProvider);
    }
}
