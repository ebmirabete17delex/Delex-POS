namespace Delex_POS.Domain.Entities;

public class UnitOfMeasure : BaseAuditableEntity
{
    public string UnitOfMeasureId { get; init; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string UnitBase { get; private set; } = string.Empty;
    public bool IsGroupAsSingleQuantity { get; private set; }
    public bool IsAsForQtyWhenSold { get; private set;  }
    public int DecimalPlaces { get; private set;  }
    public UnitOfMeasure(string unitOfMeasureId, string name, string unitBase, bool isGroupAsSingleQuantity,
        bool isAsForQtyWhenSold, int decimalPlaces)
    {
        UnitOfMeasureId = unitOfMeasureId;
        Name = name;
        UnitBase = unitBase;
        IsGroupAsSingleQuantity = isGroupAsSingleQuantity;
        IsAsForQtyWhenSold = isAsForQtyWhenSold;
        DecimalPlaces = decimalPlaces;
    }
    public void Update(string? name, string? unitBase, bool isGroupAsSingleQuantity,
        bool isAsForQtyWhenSold, int decimalPlaces)
    {
        Name = name ?? string.Empty;
        UnitBase = unitBase ?? string.Empty;
        IsGroupAsSingleQuantity = isGroupAsSingleQuantity;
        IsAsForQtyWhenSold = isAsForQtyWhenSold;
        DecimalPlaces = decimalPlaces;
    }
}
