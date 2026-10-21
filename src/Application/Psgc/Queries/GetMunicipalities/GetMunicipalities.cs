using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;

namespace Delex_POS.Application.Psgc.Queries.GetMunicipalities;

public record GetMunicipalitiesQuery : IRequest<List<MunicipalityDto>>
{
    public string ProvinceCode { get; set; } = string.Empty;
};
public class GetMunicipalitiesQueryHandler : IRequestHandler<GetMunicipalitiesQuery,List<MunicipalityDto>>
{
    private readonly IMapper _mapper;
    private readonly IPsgcMunicipalityQueryRepository _psgcMunicipalityQueryRepository;
    public GetMunicipalitiesQueryHandler(
        IMapper mapper,
        IPsgcMunicipalityQueryRepository psgcMunicipalityQueryRepository)
    {
        _mapper = mapper;
        _psgcMunicipalityQueryRepository = psgcMunicipalityQueryRepository;
    }

    public async Task<List<MunicipalityDto>> Handle(GetMunicipalitiesQuery request, CancellationToken cancellationToken)
    {
        return await _psgcMunicipalityQueryRepository.GetAll().Where(m => m.ProvinceCode == request.ProvinceCode)
            .ProjectTo<MunicipalityDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
