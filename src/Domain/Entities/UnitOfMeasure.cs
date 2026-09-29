using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class UnitOfMeasure : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public string? UnitBase { get; private set; } = string.Empty;
    public bool IsGroupAsSingleQuantity { get; private set; }
    public bool IsAsForQtyWhenSold { get; private set;  }
    public int DecimalPlaces { get; private set;  }
    public UnitOfMeasure(string name, string? unitBase, bool isGroupAsSingleQuantity,
        bool isAsForQtyWhenSold, int decimalPlaces)
    {
        Name = name;
        UnitBase = unitBase;
        IsGroupAsSingleQuantity = isGroupAsSingleQuantity;
        IsAsForQtyWhenSold = isAsForQtyWhenSold;
        DecimalPlaces = decimalPlaces;
    }
    public void Update(string? unitBase, bool isGroupAsSingleQuantity,
        bool isAsForQtyWhenSold, int decimalPlaces)
    {  
        UnitBase = unitBase;
        IsGroupAsSingleQuantity = isGroupAsSingleQuantity;
        IsAsForQtyWhenSold = isAsForQtyWhenSold;
        DecimalPlaces = decimalPlaces;
    }
}
