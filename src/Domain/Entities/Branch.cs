namespace Delex_POS.Domain.Entities;
public class Branch : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public int WarehouseId { get; private set; }
    public int RegionId { get; private set; }
    public string? Memo { get; private set; }
    public string? Address1 { get; private set; }
    public string? Address2 { get; private set; }
    public string? Address3 { get; private set; }
    public string? Phone { get; private set; }
    public string? Fax { get; private set; }
    public string? Email { get; private set; }
    public string? TIN {  get; private set; }

    public Branch(string name, int warehouseId, int regionId, string? memo, 
        string? address1, string? address2, string? address3, string? phone, string? fax, 
        string? email, string? tin)
    {
        Name = name;
        IsActive = true;
        WarehouseId = warehouseId;
        RegionId = regionId;
        Memo = memo ?? string.Empty;
        Address1 = address1 ?? string.Empty;
        Address2 = address2 ?? string.Empty;
        Address3 = address3 ?? string.Empty;
        Phone = phone ?? string.Empty;
        Fax = fax ?? string.Empty;
        Email = email ?? string.Empty;
        TIN = tin ?? string.Empty;
    }

    public void Update(string name, int warehouseId, int regionId, string? memo,
        string? address1, string? address2, string? address3, string? phone, string? fax,
        string? email, string? tin)
    {
        Name = name;
        WarehouseId = warehouseId;
        RegionId = regionId;
        Memo = memo ?? string.Empty;
        Address1 = address1 ?? string.Empty;
        Address2 = address2 ?? string.Empty;
        Address3 = address3 ?? string.Empty;
        Phone = phone ?? string.Empty;
        Fax = fax ?? string.Empty;
        Email = email ?? string.Empty;
        TIN = tin ?? string.Empty;
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

