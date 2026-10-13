using Delex_POS.Application.Customers.Queries.CustomerDTOs;
namespace Delex_POS.Application.Common.Interfaces.Repositories.Customer;

public interface ICustomerQueryRepository : IQueryHandlerBase<Domain.Entities.Customer>
{
    IQueryable<CustomerListDto> GetCustomers();
    IQueryable<CustomerSearchListDto> SearchCustomers();
}
