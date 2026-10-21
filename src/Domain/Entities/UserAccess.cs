namespace Delex_POS.Domain.Entities;
public class UserAccess : BaseAuditableEntity
{
    public string UserId { get; init; }
    public int AccessId { get; init; }
    public bool IsActive { get; private set; }
    public UserAccess(string userId, int accessId)
    {
        UserId = userId;
        AccessId = accessId;
        IsActive = true;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
}
