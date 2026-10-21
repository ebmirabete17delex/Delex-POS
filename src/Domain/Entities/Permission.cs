namespace Delex_POS.Domain.Entities;

public class Permission : BaseEntity
{
    public int AccessId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Permission (int accessId, string name)
    {
        AccessId = accessId;
        Name = name;
    }
}
