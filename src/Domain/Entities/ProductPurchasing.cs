namespace Delex_POS.Domain.Entities;

public class ProductPurchasing : BaseAuditableEntity
{
    public int ProductId { get; init; }
    public int PurchasingUnitId { get; private set; } 
    public int PrimarySupplierId { get; private set; }
    public int MaxQuantity { get; private set; }
    public int ReorderQuantity { get; private set; }
    public ProductPurchasing(int productId, int purchasingUnitId, int primarySupplierId, 
        int maxQuantity, int reorderQuantity)
    {
        ProductId = productId;
        PurchasingUnitId = purchasingUnitId;
        PrimarySupplierId = primarySupplierId;
        MaxQuantity = maxQuantity;
        ReorderQuantity = reorderQuantity;
    }
    public void Update(int purchasingUnitId, int primarySupplierId,
        int maxQuantity, int reorderQuantity)
    {
        PurchasingUnitId = purchasingUnitId;
        PrimarySupplierId = primarySupplierId;
        MaxQuantity = maxQuantity;
        ReorderQuantity = reorderQuantity;
    }
}
