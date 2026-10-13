using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;
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
}
