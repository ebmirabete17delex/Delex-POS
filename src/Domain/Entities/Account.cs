using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class Account : BaseAuditableEntity
{
    public int AccountId { get; private set; }
    public string Name { get; init; } = string.Empty;
    public string AltKey {  get; private set; } = string.Empty;
    public bool IsActive {  get; private set; } 
    public int BranchId { get; private set; }
    public bool IsRestrictToAssignedBranchOnly {  get; private set; }
    public bool IsCustomer { get; init; }
    public bool IsSupplier { get; init; }
    public bool IsCashier { get; private set; }
    public bool IsSalesClerk { get; private set; }
    public bool IsEmployee { get; private set; }
    public bool IsAllowToLogon { get; private set; }
    public string? Password { get; private set; }
    public string AccessLevel { get; private set; } = string.Empty;

    public Account(int accountId, string name, string? altKey, bool isActive, int branchId, 
        bool isRestrictToAssignedBranchOnly, bool isCustomer, bool isSupplier, bool isCashier,
        bool isSalesClerk, bool isEmployee, bool isAllowToLogon, string?  password, string accessLevel) 
    {
        AccountId = accountId;
        Name = name;
        AltKey = altKey ?? string.Empty;
        IsActive = isActive;
        BranchId = branchId;
        IsRestrictToAssignedBranchOnly = isRestrictToAssignedBranchOnly;
        IsCustomer = isCustomer;
        IsSupplier = isSupplier;
        IsCashier = isCashier;
        IsSalesClerk = IsSalesClerk;
        IsEmployee = isEmployee;
        IsAllowToLogon = isAllowToLogon;
        Password = password ?? string.Empty;
        AccessLevel = accessLevel;
    }

    public void Update(string? altKey, bool isActive, int branchId, bool isRestrictToAssignedBranchOnly,
        bool isCashier, bool isSalesClerk, bool isEmployee, bool isAllowToLogon, string? password, 
        string accessLevel)
    {
        AltKey = altKey ?? string.Empty;
        IsActive = isActive;
        BranchId = branchId;
        IsRestrictToAssignedBranchOnly = isRestrictToAssignedBranchOnly;
        IsCashier = isCashier;
        IsSalesClerk = IsSalesClerk;
        IsEmployee = isEmployee;
        IsAllowToLogon = isAllowToLogon;
        Password = password ?? string.Empty;
        AccessLevel = accessLevel;
    }
}
