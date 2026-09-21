namespace Delex_POS.Domain.Entities.RBAC;
public class Role : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Role(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public void Update(string description)
    {
        Description = description;
    }
    private readonly List<Guid> _roleAccess = new();
    public IReadOnlyCollection<Guid> RoleAccess =>
        _roleAccess.AsReadOnly();
}