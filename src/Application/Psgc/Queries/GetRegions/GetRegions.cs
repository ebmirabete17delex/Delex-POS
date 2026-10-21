using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;

namespace Delex_POS.Application.Psgc.Queries.GetRegions;

public record GetRegionsQuery : IRequest<List<RegionDto>>;
public class GetRegionsQueryHandler : IRequestHandler<GetRegionsQuery, List<RegionDto>>
{
    private readonly IMapper _mapper;
    private readonly IPsgcRegionQueryRepository _psgcRegionQueryRepository;
    public GetRegionsQueryHandler(
        IMapper mapper,
        IPsgcRegionQueryRepository psgcRegionQueryRepository)
    {
        _mapper = mapper;
        _psgcRegionQueryRepository = psgcRegionQueryRepository;
    }

    public async Task<List<RegionDto>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
    {
        return await _psgcRegionQueryRepository.GetAll()
            .ProjectTo<RegionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
