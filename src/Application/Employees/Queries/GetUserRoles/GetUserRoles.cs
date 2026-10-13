using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Employees.Queries.GetUserRoles;

public record GetUserRolesQuery() : IRequest<List<UserRoleDto>>
{
    public int Id { get; init; }
};

public class GetUserQueryHandler : IRequestHandler<GetUserRolesQuery, List<UserRoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;

    public GetUserQueryHandler(
        IMapper mapper, 
        IIdentityService identityService,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _mapper = mapper;
        _identityService = identityService;
        _employeeQueryRepository = employeeQueryRepository;
    }

    public async Task<List<UserRoleDto>> Handle(GetUserRolesQuery request,CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, ee);
        var roles = await _identityService.GetUserRolesAsync(ee.UserId.Trim());
        return _mapper.Map<List<UserRoleDto>>(roles);
    }
}
