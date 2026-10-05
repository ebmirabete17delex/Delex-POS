using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;
using Delex_POS.Application.PriceLevels.Queries.PriceLevelDTOs;
using Delex_POS.Domain.Entities;
using Delex_POS.Application.PriceLevels.Queries.GetPriceLevel;

namespace Delex_POS.Application.PriceLevels.Queries.GetPriceLevels;

public record GetPriceLevelsQuery : IRequest<List<PriceLevelDto>>;

public class GetPriceLevelsQueryHandler : IRequestHandler<GetPriceLevelsQuery, List<PriceLevelDto>>
{
    private readonly IMapper _mapper;
    private readonly IPriceLevelQueryRepository _query;

    public GetPriceLevelsQueryHandler(IMapper mapper, IPriceLevelQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<List<PriceLevelDto>> Handle(GetPriceLevelsQuery request, CancellationToken cancellationToken)
    {
        var query = _query.GetAll().ProjectTo<PriceLevelDto>(_mapper.ConfigurationProvider);
        Guard.Against.NotFound(string.Empty, query);
        return await query.ToListAsync();
    }
}
