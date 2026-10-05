using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;
using Delex_POS.Application.CustomerTypes.Queries.CustomerTypeDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.CustomerTypes.Queries.GetCustomerTypes;

public record GetCustomerTypesQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<CustomerTypeDto>>
{};

public class GetCustomerTypesQueryHandler : IRequestHandler<GetCustomerTypesQuery, PaginatedList<CustomerTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ICustomerTypeQueryRepository _query;

    public GetCustomerTypesQueryHandler(IMapper mapper, ICustomerTypeQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<PaginatedList<CustomerTypeDto>> Handle(GetCustomerTypesQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;
        Expression<Func<CustomerType, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _query.GetAll().Where(filter).OrderBy(sortBy, sortDirection);
        var projected = query.ProjectTo<CustomerTypeDto>(_mapper.ConfigurationProvider);
        return await projected.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
