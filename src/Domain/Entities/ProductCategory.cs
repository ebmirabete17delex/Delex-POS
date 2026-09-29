namespace Delex_POS.Domain.Entities;

public class ProductCategory : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public ProductCategory(string name)
    {
        Name = name;
    }
}
