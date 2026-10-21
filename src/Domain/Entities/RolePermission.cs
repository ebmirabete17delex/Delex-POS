namespace Delex_POS.Domain.Entities;

public class RolePermission : BaseEntity
{
    public string RoleId { get; init; } = string.Empty;
    public int PermissionId { get; init; }
    public RolePermission(string roleId, int permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}
