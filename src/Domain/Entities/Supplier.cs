
namespace Delex_POS.Domain.Entities;

public class Supplier : BaseAuditableEntity
{
    public string? SupplierId { get; private set; } = string.Empty;
    public int PersonalInfoId { get; init; }
    public bool IsActive { get; private set; }
    public int BranchId { get; private set; }
    public Supplier(int personalInfoId, int branchId, string? supplierId)
    {
        PersonalInfoId = personalInfoId;
        BranchId = branchId;
        IsActive = true;
        SupplierId = supplierId;
    }
    public void Update(int branchId, string? supplierId)
    {
        BranchId = branchId;
        SupplierId = supplierId;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
}
