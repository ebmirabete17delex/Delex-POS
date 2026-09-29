namespace Delex_POS.Domain.Entities;

public class ProductAttribute : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;

    public ProductAttribute(string name)
    {
        Name = name;
    }
}
