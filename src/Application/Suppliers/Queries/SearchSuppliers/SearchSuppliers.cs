using System.Linq.Expressions;
using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Application.Suppliers.Queries.SupplierDTOs;
namespace Delex_POS.Application.Suppliers.Queries.SearchSuppliers;

public record SearchSuppliersQuery(string Query) : IRequest<List<SupplierSearchListDto>>;
public class SearchSuppliersQueryHandler : IRequestHandler<SearchSuppliersQuery,List<SupplierSearchListDto>>
{
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    public SearchSuppliersQueryHandler(
        ISupplierQueryRepository supplierQueryRepository)
    {
        _supplierQueryRepository = supplierQueryRepository;
    }
    public async Task<List<SupplierSearchListDto>> Handle(SearchSuppliersQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.Query;
        Expression<Func<SupplierSearchListDto, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.FullName.Contains(searchQuery),
        };
        var query = _supplierQueryRepository.SearchSuppliers().Where(filter);
        Guard.Against.NotFound(request.Query, query);
        return await query.ToListAsync(cancellationToken);

    }
}
