namespace Delex_POS.Domain.Entities;

public class ProductClass : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public string? Memo { get; private set; } 
    public int ProductTypeId { get; private set;  }
    public int TAXTypeId { get; private set; }
    public int UnitOfMeasurementId { get; private set; }
    public int GenericNameId { get; private set; }
    public ProductClass(string name, string? memo, int productTypeId, 
        int taxtypeid, int unitOfMeasurementId, int genericNameId)
    {
        Name = name;
        Memo = memo;
        ProductTypeId = productTypeId;
        TAXTypeId = taxtypeid;
        UnitOfMeasurementId = unitOfMeasurementId;
        GenericNameId = genericNameId;
    }

    public void Update (string name, string? memo, int productTypeId,
        int taxtypeid, int unitOfMeasurementId, int genericNameId)
    {
        Memo = memo;
        ProductTypeId = productTypeId;
        TAXTypeId = taxtypeid;
        UnitOfMeasurementId = unitOfMeasurementId;
        GenericNameId = genericNameId;
    }
}
