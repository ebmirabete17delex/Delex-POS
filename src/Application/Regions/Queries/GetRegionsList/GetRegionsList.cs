using Delex_POS.Application.Common.Interfaces.Repositories.Region;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Regions.Queries.RegionDTOs;

namespace Delex_POS.Application.Regions.Queries.GetRegionsList;

public record GetRegionsListQuery : IRequest<List<RegionListDto>>;
public class GetRegionsListQueryHandler : IRequestHandler<GetRegionsListQuery, List<RegionListDto>>
{
    private readonly IRegionQueryRepository _query;
    private readonly IMapper _mapper;
    public GetRegionsListQueryHandler(IMapper mapper, IRegionQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<List<RegionListDto>> Handle(GetRegionsListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll()
            .Where(r => r.IsActive == true)
            .ProjectToListAsync<RegionListDto>(_mapper.ConfigurationProvider);
    }
}
