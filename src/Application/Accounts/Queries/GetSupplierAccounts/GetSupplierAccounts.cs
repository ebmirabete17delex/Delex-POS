using System.Linq.Expressions;
using Delex_POS.Application.Accounts.Queries.AccountDTOs;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.Accounts.Queries.GetSupplierAccounts;

public record GetSupplierAccountsQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<AccountDto>>
{ };

public class GetSupplierAccountsQueryHandler : IRequestHandler<GetSupplierAccountsQuery, PaginatedList<AccountDto>>
{
    private readonly IMapper _mapper;
    private readonly IAccountQueryRepository _accountQueryRepository;
    public GetSupplierAccountsQueryHandler(IMapper mapper, IAccountQueryRepository accountQueryRepository)
    {
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
    }
    public async Task<PaginatedList<AccountDto>> Handle(GetSupplierAccountsQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;
        Expression<Func<Account, bool>> filter = (searchQuery) switch
        {
            (null) => x => x.AccountType == AccountType.Supplier,
            (_) => x => (x.Name.Contains(searchQuery) || x.AltKey.Contains(searchQuery)) && x.AccountType == AccountType.Supplier,
        };
        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;
        var query = _accountQueryRepository.GetAll().Where(filter);
        query = query.OrderBy(sortBy, sortDirection);
        var projectedQuery = query.ProjectTo<AccountDto>(_mapper.ConfigurationProvider);
        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
