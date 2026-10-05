namespace Delex_POS.Domain.Entities;

public class ProductSale : BaseAuditableEntity
{
    public int ProductId { get; init; }
    public string? ShortName { get; private set; }
    public bool IsSellThisItem { get; private set; }
    public bool IsSellItemInWeb { get; private set; }
    public TaxType TaxType { get; private set; }
    public decimal MarkUp { get; private set;  }
    public decimal StandardCost { get; private set; }
    public decimal LastPrice { get; private set; }
    public bool SeniorTax { get; private set; } 
    public bool PwdTax { get; private set; } 
    public bool IsSubjectToAmusement { get; private set; }
    public bool IsSubjectToSoloParentDiscount { get; private set; }

    public ProductSale(int productId, string? shortName, bool isSellThisItem, 
        bool isSellItemInWeb, TaxType taxType, decimal markUp, decimal standardCost, 
        decimal lastPrice, bool seniorTax, bool pwdTax, bool isSubjectToAmusement, 
        bool isSubjectToSoloParentDiscount)
    {
        ProductId = productId;
        ShortName = shortName;
        IsSellThisItem = isSellThisItem;
        IsSellItemInWeb = isSellItemInWeb;
        TaxType = taxType;
        MarkUp = markUp;
        StandardCost = standardCost;
        LastPrice = lastPrice;
        SeniorTax = seniorTax;
        PwdTax = pwdTax;
        IsSubjectToAmusement = isSubjectToAmusement;
        IsSubjectToSoloParentDiscount = isSubjectToSoloParentDiscount;
    }

    public void Update(string? shortName, bool isSellThisItem,
        bool isSellItemInWeb, TaxType taxType, decimal markUp, decimal standardCost,
        decimal lastPrice, bool seniorTax, bool pwdTax, bool isSubjectToAmusement,
        bool isSubjectToSoloParentDiscount)
    {
        ShortName = shortName;
        IsSellThisItem = isSellThisItem;
        IsSellItemInWeb = isSellItemInWeb;
        TaxType = taxType;
        MarkUp = markUp;
        StandardCost = standardCost;
        LastPrice = lastPrice;
        SeniorTax = seniorTax;
        PwdTax = pwdTax;
        IsSubjectToAmusement = isSubjectToAmusement;
        IsSubjectToSoloParentDiscount = isSubjectToSoloParentDiscount;
    }
}
