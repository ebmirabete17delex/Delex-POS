namespace Delex_POS.Domain.Entities;
public class Product : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public int ProductClassId { get; private set; }
    public int ProductTypeId { get; private set; }
    public int UnitOfMeasureId { get; private set; }
    public string? Memo { get; private set; }
    public bool IsActive { get; private set; }

    //public string Brand { get; init; } = string.Empty;
    //public string GenericName { get; private set; } = string.Empty;
    //public string Dosage { get; private set; } = string.Empty;
    //public string Form { get; private set; } = string.Empty;
    //public string Category { get; private set; } = string.Empty;
    //public string SKU { get; private set; } = string.Empty;
    //public string Barcode { get; private set; } = string.Empty;
    //public string RxOtcClass {get; private set; } = string.Empty;
    
    public Product(string name, int productClassId, int productTypeId, int unitOfMeasureId,string? memo)
    {
        Name = name;
        ProductClassId = productClassId;
        ProductTypeId = productTypeId;
        UnitOfMeasureId = unitOfMeasureId;
        IsActive = true;
        Memo = memo ?? string.Empty;
    }

    public void Update(int productClassId, int productTypeId, int unitOfMeasureId, string? memo)
    {
        ProductClassId = productClassId;
        ProductTypeId = productTypeId;
        UnitOfMeasureId = unitOfMeasureId;
        Memo = memo;
    }

    public void Activate()
    {
        IsActive = true;
    }
    public void Dectivate()
    {
        IsActive = false;
    }
}
