namespace Delex_POS.Domain.Entities;

public class PurchaseOrderHistory : BaseAuditableEntity
{
    public int PurchaseOrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public int UserId { get; set; }
}
