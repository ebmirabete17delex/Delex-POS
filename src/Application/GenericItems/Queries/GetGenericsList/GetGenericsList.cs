using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.GenericItems.Queries.GenericDTOs;

namespace Delex_POS.Application.GenericItems.Queries.GetGenericsList;

public record GetGenericsListQuery : IRequest<List<GenericsListDto>>;
public class GetGenericsListQueryHandler : IRequestHandler<GetGenericsListQuery, List<GenericsListDto>>
{
    private readonly IGenericQueryRepository _query;
    private readonly IMapper _mapper;
    public GetGenericsListQueryHandler(IMapper mapper, IGenericQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<GenericsListDto>> Handle(GetGenericsListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll().ProjectToListAsync<GenericsListDto>(_mapper.ConfigurationProvider);
    }
}
