using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Employees.Queries.GetUserAccess;

public record GetUserAccessQuery : IRequest<List<UserAccessDto>>
{
    public int Id { get; set; } 
};

public class GetRoleAccessQueryHandler : IRequestHandler<GetUserAccessQuery, List<UserAccessDto>>
{
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;

    public GetRoleAccessQueryHandler(
        IUserAccessQueryRepository userAccessQueryRepository,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _userAccessQueryRepository = userAccessQueryRepository;
        _employeeQueryRepository = employeeQueryRepository;
    }

    public async Task<List<UserAccessDto>> Handle(GetUserAccessQuery request, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, ee);

        var accesses = _userAccessQueryRepository
            .GetByUserId(ee.UserId);
        
        return await accesses.ToListAsync();
    }
}
