namespace Delex_POS.Domain.Entities;

public class UserPermission : BaseEntity
{
    public int UserId { get; init; }
    public int PermissionId { get; init; }
    public UserPermission(int userId, int permissionId)
    {
        UserId = userId;
        PermissionId = permissionId;
    }
}
