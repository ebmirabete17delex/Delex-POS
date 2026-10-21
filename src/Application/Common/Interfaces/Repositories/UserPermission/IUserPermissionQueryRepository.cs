using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;

public interface IUserPermissionQueryRepository : IQueryHandlerBase<Domain.Entities.UserPermission>
{
    Task<IQueryable<UserPermissionDto>> GetUserPermissionsAsync(int userId, CancellationToken cancellationToken);
    Task<IQueryable<UserPermissionDto>> GetAccessPermissionsAsync(int accessId, int userId, CancellationToken cancellationToken);
}
