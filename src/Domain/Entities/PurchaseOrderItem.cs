namespace Delex_POS.Domain.Entities;

public class PurchaseOrderItem : BaseAuditableEntity
{
    public int PurchaseOrderId { get; set; }
    public int ProductId { get; set; }
    public long Quantity { get; set; }
}
