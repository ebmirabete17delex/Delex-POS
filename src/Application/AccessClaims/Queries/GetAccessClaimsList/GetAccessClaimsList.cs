using Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Mappings;

namespace Delex_POS.Application.AccessClaims.Queries.GetAccessClaimsList;

public record GetAccessClaimsListQuery : 
    IRequest<List<AccessClaimListDto>>;
public class GetAccessClaimsListQueryHandler : 
    IRequestHandler<GetAccessClaimsListQuery, List<AccessClaimListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    public GetAccessClaimsListQueryHandler(
        IMapper mapper, 
        IAccessClaimQueryRepository accessClaimQueryRepository)
    {
        _mapper = mapper;
        _accessClaimQueryRepository = accessClaimQueryRepository;
    }

    public async Task<List<AccessClaimListDto>> Handle(
        GetAccessClaimsListQuery request, 
        CancellationToken cancellationToken)
    {
        var query = _accessClaimQueryRepository.GetAll();
        return await query.ProjectToListAsync<AccessClaimListDto>(_mapper.ConfigurationProvider);
    }
}
