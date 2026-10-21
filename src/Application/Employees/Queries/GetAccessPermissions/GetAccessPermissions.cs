using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Employees.Queries.GetAccessPermissions;
public record GetAccessPermissionsQuery : IRequest<List<UserAccessPermissionDto>>
{
    public int UserId { get; set; }
}
public class GetAccessPermissionsQueryHandler : IRequestHandler<GetAccessPermissionsQuery,List<UserAccessPermissionDto>>
{
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public readonly IUserAccessQueryRepository _userAccessQueryRepository;
    private readonly IUserPermissionQueryRepository _userPermissionQueryRepository;
    public GetAccessPermissionsQueryHandler(
        IEmployeeQueryRepository employeeQueryRepository,
        IUserAccessQueryRepository userAccessQueryRepository,
        IUserPermissionQueryRepository userPermissionQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
        _userAccessQueryRepository = userAccessQueryRepository;
        _userPermissionQueryRepository = userPermissionQueryRepository;
    }

    public async Task<List<UserAccessPermissionDto>> Handle(
        GetAccessPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(request.UserId, cancellationToken);
        Guard.Against.NotFound(request.UserId, ee);

        var accessList = new List<UserAccessPermissionDto>();
        var accesses = _userAccessQueryRepository.GetByUserId(ee.UserId);
        foreach (var a in accesses)
        {
            var userAccess = new UserAccessPermissionDto
            {
                AccessId = a.AccessId,
                Name = a.Name,
                Feature = "",
                Permissions = []
            };
            var entities = await _userPermissionQueryRepository
                .GetAccessPermissionsAsync(a.AccessId, request.UserId, cancellationToken);
            Guard.Against.NotFound(a.AccessId, entities);
            foreach (var entity in entities)
            {
                userAccess.Permissions.Add(new UserPermissionDto
                {
                    UserPermissionId = entity.UserPermissionId,
                    PermisionId = entity.PermisionId,
                    Name = entity.Name,
                    Feature = entity.Feature
                });
            }
            accessList.Add(userAccess);
        }
        return accessList;
    }
}
