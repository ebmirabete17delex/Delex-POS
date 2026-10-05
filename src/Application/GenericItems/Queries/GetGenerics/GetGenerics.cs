using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Application.GenericItems.Queries.GenericDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.GenericItems.Queries.GetGenerics;

public record GetGenericsQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<GenericDto>>
{};

public class GetGenericsQueryHandler : IRequestHandler<GetGenericsQuery, PaginatedList<GenericDto>>
{
    private readonly IMapper _mapper;
    private readonly IGenericQueryRepository _query;

    public GetGenericsQueryHandler(IMapper mapper, IGenericQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<PaginatedList<GenericDto>> Handle(GetGenericsQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;
        Expression<Func<Generic, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Code.Contains(searchQuery) || x.Name.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _query.GetAll().Where(filter).OrderBy(sortBy, sortDirection);
        var projected = query.ProjectTo<GenericDto>(_mapper.ConfigurationProvider);
        return await projected.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
