namespace Delex_POS.Domain.Entities.RBAC;
public class UserAccess : BaseAuditableEntity
{
    public string UserId { get; private set; }
    public int AccessId { get; private set; }
    public UserAccess(string userId, int accessId)
    {
        UserId = userId;
        AccessId = accessId;
    }
}