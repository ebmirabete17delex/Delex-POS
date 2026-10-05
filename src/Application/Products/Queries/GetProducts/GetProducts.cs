using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Application.Products.Queries.ProductDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Products.Queries.GetProducts;

public record GetProductsQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<ProductDto>>
{};

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedList<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductQueryRepository _productQueryRepository;

    public GetProductsQueryHandler(IMapper mapper, IProductQueryRepository productQueryRepository)
    {
        _mapper = mapper;
        _productQueryRepository = productQueryRepository;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        Expression<Func<Product, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery)
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _productQueryRepository.GetAll().Where(filter);

        query = query.OrderBy(sortBy, sortDirection);

        var projectedQuery = query.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
