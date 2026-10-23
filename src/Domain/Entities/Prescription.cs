namespace Delex_POS.Domain.Entities;

public class Prescription : BaseAuditableEntity
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
}
