using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class ProductSales : BaseAuditableEntity
{
    public int ProductId { get; init; }
    public string? ShortName { get; private set; }
    public bool IsSellThisItem { get; private set; }
    public bool IsSellIteminWeb { get; private set; }
    public int TaxType { get; private set; }
    public decimal MarkUp { get; private set;  }
    public decimal StandardCost { get; private set; }
    public decimal LastPrice { get; private set; }
    public string SeniorTax { get; private set; } = string.Empty;
    public string PWDTax { get; private set; } = string.Empty;
    public bool IsSubjectToAmusement { get; private set; }
    public bool IsSubjectToSoloParentDiscount { get; private set; }

    public ProductSales(int productId, string? shortName, bool isSellThisItem, 
        bool isSellItemInWeb, int taxType, decimal markUp, decimal standardCost, 
        decimal lastPrice, string seniorTax, string pwdTax, bool isSubjectToAmusement, 
        bool isSubjectToSoloParentDiscount)
    {
        ProductId = productId;
        ShortName = shortName;
        IsSellThisItem = isSellThisItem;
        IsSellIteminWeb = isSellItemInWeb;
        TaxType = taxType;
        MarkUp = markUp;
        StandardCost = standardCost;
        LastPrice = lastPrice;
        SeniorTax = seniorTax;
        PWDTax = pwdTax;
        IsSubjectToAmusement = isSubjectToAmusement;
        IsSubjectToSoloParentDiscount = isSubjectToSoloParentDiscount;
    }

    public void Update(int productId, string? shortName, bool isSellThisItem,
        bool isSellItemInWeb, int taxType, decimal markUp, decimal standardCost,
        decimal lastPrice, string seniorTax, string pwdTax, bool isSubjectToAmusement,
        bool isSubjectToSoloParentDiscount)
    {
        ShortName = shortName;
        IsSellThisItem = isSellThisItem;
        IsSellIteminWeb = isSellItemInWeb;
        TaxType = taxType;
        MarkUp = markUp;
        StandardCost = standardCost;
        LastPrice = lastPrice;
        SeniorTax = seniorTax;
        PWDTax = pwdTax;
        IsSubjectToAmusement = isSubjectToAmusement;
        IsSubjectToSoloParentDiscount = isSubjectToSoloParentDiscount;
    }
}
