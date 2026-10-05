using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Warehouses.Queries.WarehouseDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Warehouses.Queries.GetWarehouses;

public record GetWarehousesQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<WarehouseDto>>
{ };
public class GetWarehousesQueryHandler : IRequestHandler<GetWarehousesQuery, PaginatedList<WarehouseDto>>
{
    private readonly IMapper _mapper;
    private readonly IWarehouseQueryRepository _query;
    public GetWarehousesQueryHandler(IMapper mapper, IWarehouseQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<PaginatedList<WarehouseDto>> Handle(GetWarehousesQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        Expression<Func<Warehouse, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery) || x.Email!.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _query.GetAll().Where(filter);

        query = query.OrderBy(sortBy, sortDirection);

        var projectedQuery = query.ProjectTo<WarehouseDto>(_mapper.ConfigurationProvider);

        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);

    }
}
