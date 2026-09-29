using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class ProductPurchasing : BaseAuditableEntity
{
    public int ProductId { get; init; }
    public string? PurchaseUnit { get; private set; } 
    public int PrimarySupplier { get; private set; }
    public int ExtendedSupplier { get; private set; }
    public int MaxCount { get; private set; }
    public int ReorderQuantity { get; private set; }
    public ProductPurchasing(int productId, string? purchaseUnit, int primarySupplier, 
        int extendedSupplier, int maxCount, int reorderQuantity)
    {
        ProductId = productId;
        PurchaseUnit = purchaseUnit ?? string.Empty;
        PrimarySupplier = primarySupplier;
        ExtendedSupplier = extendedSupplier;
        MaxCount = maxCount;
        ReorderQuantity = reorderQuantity;
    }
    public void Update(int productId, string? purchaseUnit, int primarySupplier,
        int extendedSupplier, int maxCount, int reorderQuantity)
    {
        PurchaseUnit = purchaseUnit ?? string.Empty;
        PrimarySupplier = primarySupplier;
        ExtendedSupplier = extendedSupplier;
        MaxCount = maxCount;
        ReorderQuantity = reorderQuantity;
    }
}
