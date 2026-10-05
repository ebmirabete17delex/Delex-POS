namespace Delex_POS.Domain.Entities;
public class RoleAccess : BaseAuditableEntity
{
    public string RoleId { get; private set; }
    public int AccessId { get; private set; }
    public AccessType Type { get; private set; }
    public RoleAccess(string roleId, int accessId, AccessType type)
    {
        RoleId = roleId;
        AccessId = accessId;
        Type = type;
    }
}
