using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Employees.Queries.GetEmployee;

public record GetEmployeeQuery(int Id) : IRequest<EmployeeDto>;
public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery,EmployeeDto>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public GetEmployeeQueryHandler(
        IMapper mapper, 
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
        _mapper = mapper;
    }
    public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var entity = await _employeeQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<EmployeeDto>(entity);
    }
}
