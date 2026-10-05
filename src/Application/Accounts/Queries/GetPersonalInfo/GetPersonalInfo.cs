using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;

namespace Delex_POS.Application.Accounts.Queries.GetPersonalInfo;

public record GetPersonalInfoQuery(int Id) : IRequest<PersonalInfoDto>;
public class GetPersonalInfoQueryHandler : IRequestHandler<GetPersonalInfoQuery, PersonalInfoDto>
{
    private readonly IMapper _mapper;
    private readonly IPersonalInfoQueryRepository _query;
    public  GetPersonalInfoQueryHandler(IMapper mapper, IPersonalInfoQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<PersonalInfoDto> Handle(GetPersonalInfoQuery request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<PersonalInfoDto>(entity);
    }
}
