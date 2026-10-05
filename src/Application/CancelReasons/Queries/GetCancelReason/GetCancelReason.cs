using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;
using Delex_POS.Application.CancelReasons.Queries.CancelReasonDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.CancelReasons.Queries.GetCancelReason;

public record GetCancelReasonQuery(int Id) : IRequest<CancelReasonDto>;

public class GetCancelReasonQueryHandler : IRequestHandler<GetCancelReasonQuery, CancelReasonDto>
{
    private readonly IMapper _mapper;
    private readonly ICancelReasonQueryRepository _cancelReasonQueryRepository;

    public GetCancelReasonQueryHandler(IMapper mapper, ICancelReasonQueryRepository cancelReasonQueryRepository)
    {
        _mapper = mapper;
        _cancelReasonQueryRepository = cancelReasonQueryRepository;
    }

    public async Task<CancelReasonDto> Handle(GetCancelReasonQuery request, CancellationToken cancellationToken)
    {
        var entity = await _cancelReasonQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<CancelReasonDto>(entity);
    }
}
