namespace Delex_POS.Domain.Entities;
public class RoleAccess : BaseAuditableEntity
{
    public string RoleId { get; init; }
    public int AccessId { get; init; }
    public bool IsActive { get; private set; }
    public RoleAccess(string roleId, int accessId)
    {
        RoleId = roleId;
        AccessId = accessId;
        IsActive = true;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public void Dectivate()
    {
        IsActive = true;
    }
}
