using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;
using Delex_POS.Application.Nationalities.Queries.NationalityDTOs;

namespace Delex_POS.Application.Nationalities.Queries.GetNationality;

public record GetNationalityQuery(int Id) : IRequest<NationalityDto>;

public class GetNationalityQueryHandler : IRequestHandler<GetNationalityQuery, NationalityDto>
{
    private readonly IMapper _mapper;
    private readonly INationalityQueryRepository _query;
    public GetNationalityQueryHandler(IMapper mapper, INationalityQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<NationalityDto> Handle(GetNationalityQuery request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<NationalityDto>(entity);
    }
}
