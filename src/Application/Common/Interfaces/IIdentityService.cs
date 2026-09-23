using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Roles.Queries.RoleDTOs;
using Delex_POS.Application.Users.Queries.GetUsers;
namespace Delex_POS.Application.Common.Interfaces;

public interface IIdentityService
{
    // User Management
    #region User management
    Task<string?> GetUserNameAsync(string userId);

    Task<ApplicationUserDto> GetUserById(string userid);
    IQueryable<ApplicationUserDto> GetAllUsers(string? searchQuery, string sortBy, TableSort sort);
    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<(Result Result, string UserId)> CreateUserAsync(ApplicationUserDto userName, string password);

    Task<Result> DeleteUserAsync(string userId);
    Task<Result> UpdateUserAsync(ApplicationUserDto user);
    #endregion

    // Role Management
    #region Role Management
    Task<(Result Result, string RoleId)> CreateRoleAsync(string roleName);
    Task<Result> DeleteRoleAsync(string roleId);
    Task<Result> AddToRoleAsync(string userId, string role);
    Task<Result> RemoveFromRoleAsync(string userId, string role);
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task<bool> RoleExistAsync(string roleName);
    Task<IList<IdentityRoleDto>> GetAllRolesAsync(CancellationToken cancellationToken);
    IQueryable<IdentityRoleDto> GetAllRoles(string? searchQuery, string sortBy, TableSort sort);
    Task<IdentityRoleDto> GetRoleByIdAsync(string id, CancellationToken cancellationToken);
    Task<IdentityRoleDto> GetRoleByNameAsync(string name, CancellationToken cancellationToken);
    #endregion

    #region Authentication
    Task<TokenResponse?> LoginAsync(string userCode, string password, bool isPersistent = false);
    #endregion
}
