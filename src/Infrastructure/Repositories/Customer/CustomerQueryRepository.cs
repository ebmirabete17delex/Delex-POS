using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Customers.Queries.CustomerDTOs;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.Customer;

public class CustomerQueryRepository : QueryHandlerBase<Domain.Entities.Customer>, ICustomerQueryRepository
{
    public CustomerQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    public IQueryable<CustomerDto> GetCustomer(int id)
    {
        string query = $"""
                        SELECT c.Id as Id
                                , c.CustomerId 
                              	, p.LastName 
                                , p.FirstName 
                                , p.MiddleName
                              	, p.BirthDate AS BirthDate
                              	, p.Gender AS Gender
                              	, p.CivilStatus AS CivilStatus
                                , c.BranchId 
                                , p.Title,
                                , p.NameExt,
                              	, p.Email AS Email
                                , p.BillingAddress1
                                , p.BillingAddress2
                                , p.RegionCode
                                , r.AreaName AS Region
                                , P.ProvinceCode
                                , s.AreaName as Province
                                , p.MunicipalityCode
                                , m.AreaName as Municipality
                                , p.BarangayCode
                                , b.AreaName as Barangay
                                , p.Phone as Phone
                                , p.Mobile as Mobile
                                , p.Fax as Fax
                                , p.TIN as TIN
                                , p.ContactPerson as ContactPerson
                                , c.Created AS Created
                        FROM Customers c
                        		INNER JOIN PersonalInfos p
                        					ON c.PersonalInfoId = p.Id
                                LEFT OUTER JOIN Regions r
                                            ON p.RegionCode = r.Code
                                LEFT OUTER JOIN Provinces s
                                            ON p.ProvinceCode = s.Code
                                LEFT OUTER JOIN Municipalities m
                                            ON p.RegionCode = m.Code
                                LEFT OUTER JOIN Barangays b
                                            ON p.RegionCode = b.Code
                        WHERE c.Id = {id}
                        """;

        return _dbContext.Database.SqlQueryRaw<CustomerDto>(query);
    }
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
