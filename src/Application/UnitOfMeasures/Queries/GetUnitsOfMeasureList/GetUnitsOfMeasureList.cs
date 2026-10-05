using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.UnitOfMeasures.Queries.UnitOfMeasureDTOs;

namespace Delex_POS.Application.UnitOfMeasures.Queries.GetUnitsOfMeasureList;

public record GetUnitsOfMeasureListQuery : IRequest<List<UnitOfMeasureListDto>>;
public class GetUnitsOfMeasureListQueryHandler : IRequestHandler<GetUnitsOfMeasureListQuery, List<UnitOfMeasureListDto>>
{
    private readonly IUnitOfMeasureQueryRepository _query;
    private readonly IMapper _mapper;
    public GetUnitsOfMeasureListQueryHandler(IMapper mapper, IUnitOfMeasureQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<UnitOfMeasureListDto>> Handle(GetUnitsOfMeasureListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll().ProjectToListAsync<UnitOfMeasureListDto>(_mapper.ConfigurationProvider);
    }
}
