namespace Delex_POS.Domain.Entities.RBAC;
public class UserAccess : BaseAuditableEntity
{
    public int UserId { get; private set; }
    public int AccessId { get; private set; }
    public UserAccess(int userId, int accessId)
    {
        UserId = userId;
        AccessId = accessId;
    }
}