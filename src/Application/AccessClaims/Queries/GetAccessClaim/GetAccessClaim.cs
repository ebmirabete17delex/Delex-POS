using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;

namespace Delex_POS.Application.AccessClaims.Queries.GetAccessClaim;

public record GetAccessClaimQuery() : IRequest<AccessClaimDto>
{
    public int Id { get; init; }
};

public class GetAccessClaimQueryHandler : IRequestHandler<GetAccessClaimQuery, AccessClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;

    public GetAccessClaimQueryHandler(IMapper mapper, IAccessClaimQueryRepository AccessClaimQueryRepository)
    {
        _mapper = mapper;
        _accessClaimQueryRepository = AccessClaimQueryRepository;
    }

    public async Task<AccessClaimDto> Handle(GetAccessClaimQuery request,CancellationToken cancellationToken)
    {
        var entity = await _accessClaimQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<AccessClaimDto>(entity);
    }
}
