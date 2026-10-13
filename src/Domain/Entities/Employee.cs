namespace Delex_POS.Domain.Entities;

public class Employee : BaseAuditableEntity
{
    public string UserId { get; init; } = string.Empty; 
    public string? EmployeeId { get; private set; } = string.Empty;
    public int PersonalInfoId { get; init; }
    public bool IsActive { get; private set; }
    public int BranchId { get; private set; }

    public Employee(string userId, int personalInfoId, int branchId, string employeeId)
    {
        UserId = userId;
        PersonalInfoId = personalInfoId;
        BranchId = branchId;
        EmployeeId = employeeId;
        IsActive = true;
    }

    public void Update(int branchId, string employeeId)
    {
        BranchId = branchId;
        EmployeeId = employeeId;
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
