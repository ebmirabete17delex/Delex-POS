namespace Delex_POS.Domain.Entities;

public class PurchaseOrder : BaseAuditableEntity
{
    public int BranchId { get; set; }
    public int SupplierId { get; set; }
}
