namespace Delex_POS.Domain.Entities.RBAC;
public class UserAccess : BaseAuditableEntity
{
    public Guid UserId { get; private set; }
    public Guid AccessId { get; private set; }
    public UserAccess(Guid userId, Guid accessId)
    {
        UserId = userId;
        AccessId = accessId;
    }
}