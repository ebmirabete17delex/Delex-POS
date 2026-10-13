
using System.Linq.Expressions;
using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Customers.Queries.CustomerDTOs;

namespace Delex_POS.Application.Customers.Queries.SearchCustomer;

public record SearchCustomerQuery(string Query) : IRequest<List<CustomerSearchListDto>>;
public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery,List<CustomerSearchListDto>> 
{
    private readonly ICustomerQueryRepository _customerQueryRepository;
    public SearchCustomerQueryHandler(ICustomerQueryRepository customerQueryRepository)
    {
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task<List<CustomerSearchListDto>> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.Query;
        Expression<Func<CustomerSearchListDto, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.FullName.Contains(searchQuery),
        };
        var query = _customerQueryRepository.SearchCustomers().Where(filter);    
        Guard.Against.NotFound(request.Query, query);
        return await query.ToListAsync(cancellationToken);
    }
}
