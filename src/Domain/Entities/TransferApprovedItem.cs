namespace Delex_POS.Domain.Entities;

public class TransferApprovedItem : BaseAuditableEntity
{
    public int ProductId { get; set; }
    public long Quantity { get; set; }
}
