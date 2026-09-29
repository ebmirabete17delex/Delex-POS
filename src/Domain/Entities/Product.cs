namespace Delex_POS.Domain.Entities;
public class Product : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public int ProductClassId { get; private set; }
    public int ProductTypeId { get; private set; }
    public int UnitOfMeasureId { get; private set; }
    public string? Memo { get; private set; }
    public bool IsActive { get; private set; }
    public int PackingId { get; private set; }

    public string Brand { get; init; } = string.Empty;
    public string GenericName { get; private set; } = string.Empty;
    public string Dosage { get; private set; } = string.Empty;
    public string Form { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public string SKU { get; private set; } = string.Empty;
    public string Barcode { get; private set; } = string.Empty;
    public string RxOtcClass {get; private set; } = string.Empty;
    
    public Product(string brand, string genericName, string dosage, string form, string category, string sku, string barCode, string rxOtcClass)
    {
        Brand = brand;
        GenericName = genericName;
        Dosage = dosage;
        Form = form;
        Category = category;
        SKU = sku;
        Barcode = barCode;
        RxOtcClass = rxOtcClass;
    }

    public void Update(string genericName, string dosage, string form, string category, string sku, string barCode, string rxOtcClass)
    {
        GenericName = genericName;
        Dosage = dosage;
        Form = form;
        Category = category;
        SKU = sku;
        Barcode = barCode;
        RxOtcClass = rxOtcClass;
    }
}
