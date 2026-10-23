using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Suppliers.Queries.SupplierDTOs;

namespace Delex_POS.Application.Suppliers.Queries.GetSuppliers;

public record GetSuppliersQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<SupplierListDto>>
{ };
public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, PaginatedList<SupplierListDto>>
{
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    public GetSuppliersQueryHandler(
        ISupplierQueryRepository supplierQueryRepository
        )
    {
        _supplierQueryRepository = supplierQueryRepository;
    }
    public async Task<PaginatedList<SupplierListDto>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        Expression < Func < SupplierListDto, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.FullName.Contains(searchQuery)
                    || x.BirthDate.ToString().Contains(searchQuery)
                    || nameof(x.Gender).Contains(searchQuery)
                    || nameof(x.CivilStatus).Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _supplierQueryRepository.GetSuppliers()
            .Where(filter);
        query = query.OrderBy(sortBy, sortDirection);
        return await query.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
