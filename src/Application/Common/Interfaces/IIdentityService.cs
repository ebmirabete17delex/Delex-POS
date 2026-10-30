using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Roles.Queries.RoleDTOs;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;
using Microsoft.AspNetCore.Http;
namespace Delex_POS.Application.Common.Interfaces;

public interface IIdentityService
{
    // User Management
    string? Id { get; }

    #region User management
    Task<string?> GetUserNameAsync(string userId);

    Task<ApplicationUserDto> GetUserByIdAsync(string userid);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<string> GetLastUserCode(CancellationToken cancellationToken);
    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
    Task<(Result Result, string UserId)> CreateUserAsync(ApplicationUserDto userName, string password);
    Task<Result> DeleteUserAsync(string userId);
    Task<Result> UpdateUserPinAsync(string userId, string newPIN);
    Task<Result> ActivateUserAsync(string userId, string newPassword, string userPin);
    Task<Result> DeactivateUserAsync(string userId);
    #endregion

    // Role Management
    #region Role Management
    Task<(Result Result, string RoleId)> CreateRoleAsync(string roleName);
    Task<Result> DeleteRoleAsync(string roleId);
    Task<Result> AddToRoleAsync(string userId, string role);
    Task<Result> RemoveFromRoleAsync(string userId, string role);
    Task<IList<UserRoleDto>> GetUserRolesAsync(string userId);
    Task<bool> RoleExistAsync(string roleName);
    Task<IList<IdentityRoleDto>> GetAllRolesAsync(CancellationToken cancellationToken);
    IQueryable<IdentityRoleDto> GetAllRoles(string? searchQuery, string sortBy, TableSort sort);
    Task<IdentityRoleDto> GetRoleByIdAsync(string id, CancellationToken cancellationToken);
    Task<IdentityRoleDto> GetRoleByNameAsync(string name, CancellationToken cancellationToken);
    #endregion

    #region Authentication
    Task<TokenResponse?> LoginAsync(string userCode, string password, bool isPersistent = false);
    //Task<TokenResponse?> LoginAsync(string userCode, string password, HttpContext ctx, bool isPersistent = false);
    //Task<Result> LogoutAsync(HttpContext ctx);
    Task<TokenResponse?> ActivateAsync(string userCode, string password, string pin);
    Task<ActivationResponse?> ActivateWoPINAsync(string userCode, string oldPassword, string newPassword);
    //Task<Result> RefreshAsync(HttpContext ctx);
    #endregion
}
