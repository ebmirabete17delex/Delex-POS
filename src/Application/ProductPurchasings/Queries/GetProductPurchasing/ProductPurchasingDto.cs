using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.ProductPurchasings.Queries.GetProductPurchasing;
public class ProductPurchasingDto
{
    public string? PurchaseUnit { get; set; }
    public int PrimarySupplier { get; set; }
    public int ExtendedSupplier { get; set; }
    public int MaxCount { get; set; }
    public int ReorderQuantity { get; set; }
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductPurchasing, ProductPurchasingDto>();
        }
    }
};
