using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;
using Delex_POS.Application.ProductAttributes.Queries.ProductAttributeDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.ProductAttributes.Queries.GetProductAttributes;

public record GetProductAttributesQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<ProductAttributeDto>>
{};

public class GetProductAttributesQueryHandler : IRequestHandler<GetProductAttributesQuery, PaginatedList<ProductAttributeDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductAttributeQueryRepository _query;

    public GetProductAttributesQueryHandler(IMapper mapper, IProductAttributeQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<PaginatedList<ProductAttributeDto>> Handle(GetProductAttributesQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;
        Expression<Func<ProductAttribute, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _query.GetAll().Where(filter).OrderBy(sortBy, sortDirection);
        var projected = query.ProjectTo<ProductAttributeDto>(_mapper.ConfigurationProvider);
        return await projected.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
