namespace Delex_POS.Domain.Entities;

public class CustomerType : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;

    public CustomerType(string name)
    {
        Name = name;
    }
}
