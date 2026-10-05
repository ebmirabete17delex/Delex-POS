using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;
using Delex_POS.Application.PriceLevels.Queries.PriceLevelDTOs;

namespace Delex_POS.Application.PriceLevels.Queries.GetPriceLevel;

public record GetPriceLevelQuery(int Id) : IRequest<PriceLevelDto>;
public class GetPriceLevelQueryHandler : IRequestHandler<GetPriceLevelQuery, PriceLevelDto>
{
    private readonly IMapper _mapper;
    private readonly IPriceLevelQueryRepository _query;
    public GetPriceLevelQueryHandler(IMapper mapper, IPriceLevelQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<PriceLevelDto> Handle(GetPriceLevelQuery request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<PriceLevelDto>(entity);
    }
}
