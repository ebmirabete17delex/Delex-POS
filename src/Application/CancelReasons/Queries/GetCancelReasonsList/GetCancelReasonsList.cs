using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;
using Delex_POS.Application.Common.Mappings;

using Delex_POS.Application.CancelReasons.Queries.CancelReasonDTOs;

namespace Delex_POS.Application.CancelReasons.Queries.GetCancelReasonsList;

public record GetCancelReasonsListQuery : IRequest<List<CancelReasonListDto>>;
public class GetCancelReasonsListQueryHandler : IRequestHandler<GetCancelReasonsListQuery, List<CancelReasonListDto>>
{
    private readonly ICancelReasonQueryRepository _query;
    private readonly IMapper _mapper;
    public GetCancelReasonsListQueryHandler(IMapper mapper, ICancelReasonQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<CancelReasonListDto>> Handle(GetCancelReasonsListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll()
            .Where(cr => cr.IsActive == true)
            .ProjectToListAsync<CancelReasonListDto>(_mapper.ConfigurationProvider);
    }
}
