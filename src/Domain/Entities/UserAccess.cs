namespace Delex_POS.Domain.Entities;
public class UserAccess : BaseAuditableEntity
{
    public string UserId { get; private set; }
    public int AccessId { get; private set; }
    public AccessType Type { get; private set;  }
    public UserAccess(string userId, int accessId, AccessType type)
    {
        UserId = userId;
        AccessId = accessId;
        Type = type;
    }
}
