using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Common.Interfaces.Repositories.Employee;

public interface IEmployeeQueryRepository : IQueryHandlerBase<Domain.Entities.Employee>
{
    IQueryable<EmployeeListDto> GetEmployees();
    IQueryable<EmployeeSearchListDto> SearchEmployees();
    IQueryable<EmployeeDto> GetEmployee(int id);
}
