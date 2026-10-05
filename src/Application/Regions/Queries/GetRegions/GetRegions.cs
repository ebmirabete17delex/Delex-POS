using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.Region;
using Delex_POS.Application.Regions.Queries.RegionDTOs;
namespace Delex_POS.Application.Regions.Queries.GetRegions;

public record GetRegionsQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<RegionDto>>
{ };
public class GetRegionsQueryHandler : IRequestHandler<GetRegionsQuery, PaginatedList<RegionDto>>

{
    private readonly IMapper _mapper;
    private readonly IRegionQueryRepository _regionQueryRepository;

    public GetRegionsQueryHandler(IMapper mapper, IRegionQueryRepository regionQueryRepository)
    {
        _mapper = mapper;
        _regionQueryRepository = regionQueryRepository;
    }

    public async Task<PaginatedList<RegionDto>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;
        Expression<Func<Domain.Entities.Region, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery)
        };
        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;
        var query = _regionQueryRepository.GetAll().Where(filter);
        query = query.OrderBy(sortBy, sortDirection);
        var projectedQuery = query.ProjectTo<RegionDto>(_mapper.ConfigurationProvider);
        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
