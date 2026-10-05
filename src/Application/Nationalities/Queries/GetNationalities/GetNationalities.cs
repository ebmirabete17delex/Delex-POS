using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Nationalities.Queries.NationalityDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Nationalities.Queries.GetNationalities;

public record GetNationalitiesQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<NationalityDto>>
{ };

public class GetNationalitiesQueryHandler : IRequestHandler<GetNationalitiesQuery, PaginatedList<NationalityDto>>
{
    private readonly IMapper _mapper;
    private readonly INationalityQueryRepository _query;
    public GetNationalitiesQueryHandler(IMapper mapper, INationalityQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<PaginatedList<NationalityDto>> Handle(GetNationalitiesQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;
        Expression<Func<Nationality, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery) || x.LocalName!.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _query.GetAll().Where(filter).OrderBy(sortBy, sortDirection);
        var projected = query.ProjectTo<NationalityDto>(_mapper.ConfigurationProvider);
        return await projected.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
