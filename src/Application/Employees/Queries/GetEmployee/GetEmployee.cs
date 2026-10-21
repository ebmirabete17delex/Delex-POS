using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Employees.Queries.GetEmployee;

public record GetEmployeeQuery(int Id) : IRequest<EmployeeDto>;
public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery,EmployeeDto>
{
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public GetEmployeeQueryHandler(
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
    }
    public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var entity = await _employeeQueryRepository
            .GetEmployee(request.Id)
            .FirstAsync(e => e.Id == request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return entity;
    }
}
