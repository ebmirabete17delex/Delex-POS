using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Employees.Queries.GetUserPermissions;

public record GetUserPermissionsQuery(int UserId) : IRequest<List<UserPermissionDto>>;

public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly IUserPermissionQueryRepository _userPermissionQueryRepository;
    public GetUserPermissionsQueryHandler(IUserPermissionQueryRepository userPermissionQueryRepository)
    {
        _userPermissionQueryRepository = userPermissionQueryRepository;
    }
    public async Task<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _userPermissionQueryRepository.GetUserPermissionsAsync(request.UserId, cancellationToken);
        return await entities.ToListAsync(cancellationToken);
    }
}
