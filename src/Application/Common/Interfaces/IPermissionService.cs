using Delex_POS.Application.Common.Models;

namespace Delex_POS.Application.Common.Interfaces;

public interface IPermissionService
{
    Task<bool> UserHasPermissionAsync(string userId, string resource, string action);
    Task<List<PermissionDto>> GetUserPermissionsAsync(string userId);
}
