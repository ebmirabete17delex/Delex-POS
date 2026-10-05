using Delex_POS.Application.ProductClasses.Queries.ProductClassDTOs;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.ProductClasses.Queries.GetProductClasses;

public record GetProductClassesQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<ProductClassDto>>
{ };

public class GetProductClassesQueryHandler : IRequestHandler<GetProductClassesQuery, PaginatedList<ProductClassDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductClassQueryRepository _productClassQueryRepository;

    public GetProductClassesQueryHandler(IMapper mapper, IProductClassQueryRepository productClassQueryRepository)
    {
        _mapper = mapper;
        _productClassQueryRepository = productClassQueryRepository;
    }

    public async Task<PaginatedList<ProductClassDto>> Handle(GetProductClassesQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;
        Expression<Func<ProductClass, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery),
        };
        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;
        var query = _productClassQueryRepository.GetAll().Where(filter);
        query = query.OrderBy(sortBy, sortDirection);
        var projectedQuery = query.ProjectTo<ProductClassDto>(_mapper.ConfigurationProvider);
        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
