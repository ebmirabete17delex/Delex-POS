using System.Linq.Expressions;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Employees.Queries.SearchEmployee;

public record SearchEmployeeQuery(string Query) : IRequest<List<EmployeeSearchListDto>>;
public class SearchEmployeeQueryHandler : IRequestHandler<SearchEmployeeQuery,List<EmployeeSearchListDto>>
{
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public SearchEmployeeQueryHandler(IEmployeeQueryRepository employeeQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
    }
    public async Task<List<EmployeeSearchListDto>> Handle(SearchEmployeeQuery request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.Query;
        Expression<Func<EmployeeSearchListDto, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.FullName.Contains(searchQuery),
        };
        var query = _employeeQueryRepository.SearchEmployees().Where(filter);
        Guard.Against.NotFound(request.Query, query);
        return await query.ToListAsync(cancellationToken);
    }
}
