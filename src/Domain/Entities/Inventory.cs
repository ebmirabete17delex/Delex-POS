namespace Delex_POS.Domain.Entities;

public class Inventory : BaseAuditableEntity
{
    public string BatchNo { get; set; } = String.Empty;
    public int ProductId { get; set; }
    public long Balance { get; set; }
    public DateOnly ExpiryDate { get; set; }
}
