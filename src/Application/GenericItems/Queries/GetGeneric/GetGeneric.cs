using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Application.GenericItems.Queries.GenericDTOs;

namespace Delex_POS.Application.GenericItems.Queries.GetGeneric;

public record GetGenericQuery(int Id) : IRequest<GenericDto>;

public class GetGenericQueryHandler : IRequestHandler<GetGenericQuery, GenericDto>
{
    private readonly IMapper _mapper;
    private readonly IGenericQueryRepository _query;

    public GetGenericQueryHandler(IMapper mapper, IGenericQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<GenericDto> Handle(GetGenericQuery request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<GenericDto>(entity);
    }
}
