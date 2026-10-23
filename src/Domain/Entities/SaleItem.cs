namespace Delex_POS.Domain.Entities;

public class SaleItem : BaseAuditableEntity
{
    public int SalesId { get; set; }
    public int ProductId { get; set; }
    public long Quantity { get; set; }
    public decimal TotalAmount { get; set; }
}
