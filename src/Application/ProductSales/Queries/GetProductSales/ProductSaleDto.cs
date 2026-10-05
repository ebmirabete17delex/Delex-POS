using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.ProductSales.Queries.GetProductSale;

public class ProductSaleDto
{
    public string? ShortName { get; set; }
    public bool IsSellThisItem { get; set; }
    public bool IsSellItemInWeb { get; set; }
    public TaxType TaxType { get; set; }
    public decimal MarkUp { get; set; }
    public decimal StandardCost { get; set; }
    public decimal LastPrice { get; set; }
    public bool SeniorTax { get; set; }
    public bool PwdTax { get; set; }
    public bool IsSubjectToAmusement { get; set; }
    public bool IsSubjectToSoloParentDiscount { get; set; }
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductSale, ProductSaleDto>();
        }
    }
}
