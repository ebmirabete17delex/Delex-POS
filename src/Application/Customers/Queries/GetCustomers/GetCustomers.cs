using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Customers.Queries.CustomerDTOs;

namespace Delex_POS.Application.Customers.Queries.GetCustomers;

public record GetCustomersQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<CustomerListDto>>
{ };
public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PaginatedList<CustomerListDto>>
{
    private readonly ICustomerQueryRepository _customerQueryRepository;
    public GetCustomersQueryHandler (
        ICustomerQueryRepository customerQueryRepository
        )
    {
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task<PaginatedList<CustomerListDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)        
    {
        string? searchQuery = request.SearchQuery;

        Expression<Func<CustomerListDto, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.FullName.Contains(searchQuery)
                    || x.BirthDate.ToString().Contains(searchQuery)
                    || x.Gender.ToString().Contains(searchQuery)
                    || x.CivilStatus.ToString().Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _customerQueryRepository.GetCustomers()
            .Where(filter);
        query = query.OrderBy(sortBy, sortDirection);
        return await query.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
