using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Customers.Queries.CustomerDTOs;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.Customer;

public class CustomerQueryRepository : QueryHandlerBase<Domain.Entities.Customer>, ICustomerQueryRepository
{
    public CustomerQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryable<CustomerListDto> GetCustomers()
    {
        string query = $"""
                        SELECT c.Id as Id
                              	, RTRIM(p.LastName + ' '
                                    		+ p.FirstName + ' '
                                    		+ p.MiddleName) AS FullName
                              	, p.BirthDate AS BirthDate
                              	, p.Gender AS Gender
                              	, p.CivilStatus AS CivilStatus
                              	, p.Email AS Email
                                , p.Phone as Phone
                              	, c.Created AS Created
                        FROM Customers c
                        		INNER JOIN PersonalInfos p
                        					ON c.PersonalInfoId = p.Id
                        """;

        return _dbContext.Database.SqlQueryRaw<CustomerListDto>(query);
    }
    public IQueryable<CustomerSearchListDto> SearchCustomers()
    {
        string query = $"""
                        SELECT c.Id as Id
                              	, RTRIM(p.LastName + ' '
                                    		+ p.FirstName + ' '
                                    		+ p.MiddleName) AS FullName
                        FROM Customers c
                        		INNER JOIN PersonalInfos p
                        					ON c.PersonalInfoId = p.Id
                        """;

        return _dbContext.Database.SqlQueryRaw<CustomerSearchListDto>(query);
    }
}
