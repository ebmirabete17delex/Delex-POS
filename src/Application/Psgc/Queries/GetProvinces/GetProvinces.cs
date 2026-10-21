using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;

namespace Delex_POS.Application.Psgc.Queries.GetProvinces;

public record GetProvincesQuery : IRequest<List<ProvinceDto>>
{
    public string RegionCode { get; set; } = string.Empty;
};
public class GetProvincesQueryHandler : IRequestHandler<GetProvincesQuery,List<ProvinceDto>>
{
    private readonly IMapper _mapper;
    private readonly IPsgcProvinceQueryRepository _psgcProvinceQueryRepository;
    public GetProvincesQueryHandler(
        IMapper mapper,
        IPsgcProvinceQueryRepository psgcProvinceQueryRepository)
    {
        _mapper = mapper;
        _psgcProvinceQueryRepository = psgcProvinceQueryRepository;
    }
    public async Task<List<ProvinceDto>> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
    {
        return await _psgcProvinceQueryRepository.GetAll().Where(p => p.RegionCode == request.RegionCode)
            .ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
