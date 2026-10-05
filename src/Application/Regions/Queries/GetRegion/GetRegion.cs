using Delex_POS .Application.Common.Interfaces.Repositories.Region;
using Delex_POS.Application.Regions.Queries.RegionDTOs;
namespace Delex_POS.Application.Regions.Queries.GetRegion;

public record GetRegionQuery(int Id) : IRequest<RegionDto>;
public class GetRegionQueryHandler : IRequestHandler<GetRegionQuery, RegionDto>
{
    private readonly IMapper _mapper;
    private readonly IRegionQueryRepository _regionQueryRepository;

    public GetRegionQueryHandler(IMapper mapper, IRegionQueryRepository regionQueryRepository)
    {
        _mapper = mapper;
        _regionQueryRepository = regionQueryRepository;
    }

    public async Task<RegionDto> Handle(GetRegionQuery request, CancellationToken cancellationToken)
    {
        var entity = await _regionQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<RegionDto>(entity);
    }
}
