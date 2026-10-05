namespace Delex_POS.Domain.Entities;

public class ProductClass : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public string? Memo { get; private set; } 
    public int ProductTypeId { get; private set;  }
    public TaxType TaxTypeId { get; private set; }
    public int UnitOfMeasurementId { get; private set; }
    public int GenericNameId { get; private set; }
    public ProductClass(string name, string? memo, int productTypeId, 
        TaxType taxTypeId, int unitOfMeasurementId, int genericNameId)
    {
        Name = name;
        Memo = memo;
        ProductTypeId = productTypeId;
        TaxTypeId = taxTypeId;
        UnitOfMeasurementId = unitOfMeasurementId;
        GenericNameId = genericNameId;
    }

    public void Update (string? memo, int productTypeId,
        TaxType taxTypeId, int unitOfMeasurementId, int genericNameId)
    {
        Memo = memo;
        ProductTypeId = productTypeId;
        TaxTypeId = taxTypeId;
        UnitOfMeasurementId = unitOfMeasurementId;
        GenericNameId = genericNameId;
    }
}
