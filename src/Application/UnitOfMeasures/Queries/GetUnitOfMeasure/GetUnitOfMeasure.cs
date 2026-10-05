using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
using Delex_POS.Application.UnitOfMeasures.Queries.UnitOfMeasureDTOs;

namespace Delex_POS.Application.UnitOfMeasures.Queries.GetUnitOfMeasure;

public record GetUnitOfMeasureQuery(int Id) : IRequest<UnitOfMeasureDto>;
public class GetUnitOfMeasureQueryHandler : IRequestHandler<GetUnitOfMeasureQuery, UnitOfMeasureDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQueryRepository;

    public GetUnitOfMeasureQueryHandler(
        IMapper mapper, 
        IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository)
    {
        _mapper = mapper;
        _unitOfMeasureQueryRepository = unitOfMeasureQueryRepository;
    }

    public async Task<UnitOfMeasureDto> Handle(
        GetUnitOfMeasureQuery request, 
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfMeasureQueryRepository
            .GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<UnitOfMeasureDto>(entity);
    }
}
