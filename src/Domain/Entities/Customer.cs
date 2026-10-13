namespace Delex_POS.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string? CustomerId { get; private set; } = string.Empty;
    public int PersonalInfoId { get; init; }
    public bool IsActive { get; private set; }
    public int BranchId { get; private set; }
    public int CustomerTypeId { get; private set; }
    public Customer(int personalInfoId, int branchId, int customerTypeId, string customerId)
    {
        PersonalInfoId = personalInfoId;
        IsActive = true;
        BranchId = branchId;
        CustomerTypeId = customerTypeId;
        CustomerId = customerId;
    }

    public void Update(int branchId, int customerTypeId, string customerId)
    {
        BranchId = branchId;
        CustomerTypeId = customerTypeId;
        CustomerId = customerId;
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
