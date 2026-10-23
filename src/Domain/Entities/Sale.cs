namespace Delex_POS.Domain.Entities;

public class Sale : BaseAuditableEntity
{
    public int BranchId { get; set; }
    public decimal TotalSales { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal Cash { get; set; }
    public decimal Card { get; set; }    
}
