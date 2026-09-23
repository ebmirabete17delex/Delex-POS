namespace Delex_POS.Domain.Entities.RBAC;
public class RoleAccess : BaseAuditableEntity
{
    public string RoleId { get; private set; }
    public int AccessId { get; private set; }
    public RoleAccess(string roleId, int accessId)
    {
        RoleId = roleId;
        AccessId = accessId;
    }
}