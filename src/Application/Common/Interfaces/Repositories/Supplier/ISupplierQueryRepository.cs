using Delex_POS.Application.Suppliers.Queries.SupplierDTOs;

namespace Delex_POS.Application.Common.Interfaces.Repositories.Supplier;

public interface ISupplierQueryRepository : IQueryHandlerBase<Domain.Entities.Supplier>
{
    IQueryable<SupplierListDto> GetSuppliers();
    IQueryable<SupplierSearchListDto> SearchSuppliers();
}
