namespace Delex_POS.Domain.Entities.RBAC;
public class UserRole : BaseAuditableEntity
{
    public int UserId { get; private set; }
    public int RoleId { get; private set; }
    public UserRole(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}