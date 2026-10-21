using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;
using Delex_POS.Application.Suppliers.Queries.SupplierDTOs;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.Employee;

public class EmployeeQueryRepository : QueryHandlerBase<Domain.Entities.Employee>, IEmployeeQueryRepository
{
    public EmployeeQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    public IQueryable<EmployeeListDto> GetEmployees()
    {
        string query = $"""
                        SELECT e.Id as Id
                              	, (p.LastName + ' '
                                    		+ p.FirstName + ' '
                                    		+ p.MiddleName) AS FullName
                              	, p.BirthDate AS BirthDate
                              	, p.Gender AS Gender
                              	, p.CivilStatus AS CivilStatus
                              	, p.Email AS Email
                                , p.Phone as Phone
                              	, e.Created AS Created
                        FROM Employees e
                        		INNER JOIN PersonalInfos p
                        					ON e.PersonalInfoId = p.Id
                        """;

        return _dbContext.Database.SqlQueryRaw<EmployeeListDto>(query);
    }
    public IQueryable<EmployeeSearchListDto> SearchEmployees()
    {
        string query = $"""
                        SELECT e.Id as Id
                              	, (p.LastName + ' '
                                    		+ p.FirstName + ' '
                                    		+ p.MiddleName) AS FullName
                        FROM Employees e
                        		INNER JOIN PersonalInfos p
                        					ON e.PersonalInfoId = p.Id
                        """;

        return _dbContext.Database.SqlQueryRaw<EmployeeSearchListDto>(query);
    }

    public IQueryable<EmployeeDto> GetEmployee(int id)
    {
        string query = $"""
                        SELECT e.Id as Id
                                , e.EmployeeId 
                              	, p.LastName 
                                , p.FirstName 
                                , p.MiddleName
                              	, p.BirthDate AS BirthDate
                              	, p.Gender AS Gender
                              	, p.CivilStatus AS CivilStatus
                                , e.BranchId 
                                , p.Title,
                                , p.NameExt,
                              	, p.Email AS Email
                                , p.BillingAddress1
                                , p.BillingAddress2
                                , p.RegionCode
                                , r.AreaName AS Region
                                , P.ProvinceCode
                                , c.AreaName as Province
                                , p.MunicipalityCode
                                , m.AreaName as Municipality
                                , p.BarangayCode
                                , b.AreaName as Barangay
                                , p.Phone as Phone
                                , p.Mobile as Mobile
                                , p.Fax as Fax
                                , p.TIN as TIN
                                , p.ContactPerson as ContactPerson
                                , e.Created AS Created
                        FROM Employee e
                        		INNER JOIN PersonalInfos p
                        					ON e.PersonalInfoId = p.Id
                                LEFT OUTER JOIN Regions r
                                            ON p.RegionCode = r.Code
                                LEFT OUTER JOIN Provinces c
                                            ON p.ProvinceCode = c.Code
                                LEFT OUTER JOIN Municipalities m
                                            ON p.RegionCode = m.Code
                                LEFT OUTER JOIN Barangays b
                                            ON p.RegionCode = b.Code
                        WHERE e.Id = {id}
                        """;

        return _dbContext.Database.SqlQueryRaw<EmployeeDto>(query);
    }
}
