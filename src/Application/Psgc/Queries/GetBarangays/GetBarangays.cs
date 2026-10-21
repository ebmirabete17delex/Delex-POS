using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;

namespace Delex_POS.Application.Psgc.Queries.GetBarangays;

public record GetBarangaysQuery : IRequest<List<BarangayDto>> 
{
    public string MunicipalityCode { get; set; } = string.Empty;
};
public class GetBarangaysQueryHandler : IRequestHandler<GetBarangaysQuery, List<BarangayDto>>
{
    private readonly IMapper _mapper;
    private readonly IPsgcBarangayQueryRepository _psgcBarangayQueryRepository;
    public GetBarangaysQueryHandler(
        IMapper mapper,
        IPsgcBarangayQueryRepository psgcBarangayQueryRepository)
    {
        _mapper = mapper;
        _psgcBarangayQueryRepository = psgcBarangayQueryRepository;
    }
    public async Task<List<BarangayDto>> Handle(GetBarangaysQuery request, CancellationToken cancellationToken)
    {
        return await _psgcBarangayQueryRepository.GetAll().Where(e => e.MunicipalityCode == request.MunicipalityCode)
            .ProjectTo<BarangayDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
