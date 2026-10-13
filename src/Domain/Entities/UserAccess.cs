namespace Delex_POS.Domain.Entities;
public class UserAccess : BaseAuditableEntity
{
    public string UserId { get; init; }
    public int AccessId { get; init; }
    public bool HasRead { get; private set; }
    public bool HasWrite { get; private set; }
    public bool HasDelete { get; private set; }
    public bool HasUpdate { get; private set; }
    public bool IsActive { get; private set; }
    public UserAccess(string userId, int accessId, bool hasWrite = false, bool hasDelete = false, bool hasUpdate = false)
    {
        UserId = userId;
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
