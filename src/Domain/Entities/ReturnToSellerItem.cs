namespace Delex_POS.Domain.Entities;

public class ReturnToSellerItem : BaseAuditableEntity
{
    public int ReturnToSellerId { get; set; }
    public int ProductId { get; set; }
    public long Quantity { get; set; }
}
