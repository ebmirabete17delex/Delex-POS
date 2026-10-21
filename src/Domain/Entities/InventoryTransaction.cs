namespace Delex_POS.Domain.Entities;

public class InventoryTransaction : BaseAuditableEntity
{
    public int InventoryId { get; set; }
    public InventoryTransactionType Type { get; set; }
    public string BatchNo { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public EntryType EntryType { get; set; }
}
