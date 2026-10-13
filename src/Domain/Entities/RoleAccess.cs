namespace Delex_POS.Domain.Entities;
public class RoleAccess : BaseAuditableEntity
{
    public string RoleId { get; private set; }
    public int AccessId { get; private set; }
    public bool HasRead { get; private set; }
    public bool HasWrite { get; private set; }
    public bool HasDelete { get; private set; }
    public bool HasUpdate { get; private set; }
    public bool IsActive { get; private set; }
    public RoleAccess(string roleId, int accessId, bool hasWrite = false, bool hasDelete = false, bool hasUpdate = false)
    {
        RoleId = roleId;
        AccessId = accessId;
        HasRead = true;
        HasWrite = hasWrite;
        HasDelete = hasDelete;
        HasUpdate = hasUpdate;
        IsActive = true;
    }
    public void Update(bool hasRead, bool hasWrite, bool hasDelete, bool hasUpdate, bool isActive)
    {
        HasRead = hasRead;
        HasWrite = hasWrite;
        HasDelete = hasDelete;
        HasUpdate = hasUpdate;
        IsActive = isActive;
    }
}
