using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Nationalities.Queries.NationalityDTOs;

namespace Delex_POS.Application.Nationalities.Queries.GetNationalitiesList;

public record GetNationalitiesListQuery : IRequest<List<NationalityListDto>>;
public class GetNationalitiesListQueryHandler : IRequestHandler<GetNationalitiesListQuery, List<NationalityListDto>>
{
    private readonly INationalityQueryRepository _query;
    private readonly IMapper _mapper;
    public GetNationalitiesListQueryHandler(IMapper mapper, INationalityQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<NationalityListDto>> Handle(GetNationalitiesListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll().ProjectToListAsync<NationalityListDto>(_mapper.ConfigurationProvider);
    }
}
