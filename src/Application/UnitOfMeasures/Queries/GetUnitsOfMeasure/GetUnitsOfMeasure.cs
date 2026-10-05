using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
using Delex_POS.Application.UnitOfMeasures.Queries.UnitOfMeasureDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.UnitOfMeasures.Queries.GetUnitsOfMeasure;

public record GetUnitsOfMeasureQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<UnitOfMeasureDto>>
{ }
public class GetUnitsOfMeasureQueryHandler : IRequestHandler<GetUnitsOfMeasureQuery, PaginatedList<UnitOfMeasureDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQueryRepository;

    public GetUnitsOfMeasureQueryHandler(IMapper mapper, IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository)
    {
        _mapper = mapper;
        _unitOfMeasureQueryRepository = unitOfMeasureQueryRepository;
    }

    public async Task<PaginatedList<UnitOfMeasureDto>> Handle(GetUnitsOfMeasureQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;
        Expression<Func<UnitOfMeasure, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery)
        };
        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;
        var query = _unitOfMeasureQueryRepository.GetAll().Where(filter);
        query = query.OrderBy(sortBy, sortDirection);
        var projectedQuery = query.ProjectTo<UnitOfMeasureDto>(_mapper.ConfigurationProvider);
        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
