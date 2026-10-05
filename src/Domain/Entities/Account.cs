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
    public AccountType AccountType { get; private set; }
    public bool IsAllowToLogon { get; private set; }
    public string? Password { get; private set; }
    public string AccessLevel { get; private set; } = string.Empty;

    public Account(int accountId, string name, string? altKey, int branchId, 
        bool isRestrictToAssignedBranchOnly, AccountType accountType, 
        bool isAllowToLogon, string?  password, string accessLevel) 
    {
        AccountId = accountId;
        Name = name;
        AltKey = altKey ?? string.Empty;
        IsActive = true;
        BranchId = branchId;
        IsRestrictToAssignedBranchOnly = isRestrictToAssignedBranchOnly;
        AccountType = accountType;
        IsAllowToLogon = isAllowToLogon;
        Password = password ?? string.Empty;
        AccessLevel = accessLevel;
    }

    public void Update(string? altKey, bool isActive, int branchId, 
        bool isRestrictToAssignedBranchOnly, AccountType accountType, 
        bool isAllowToLogon, string? password, string accessLevel)
    {
        AltKey = altKey ?? string.Empty;
        IsActive = isActive;
        BranchId = branchId;
        IsRestrictToAssignedBranchOnly = isRestrictToAssignedBranchOnly;
        AccountType = accountType;
        IsAllowToLogon = isAllowToLogon;
        Password = password ?? string.Empty;
        AccessLevel = accessLevel;
    }
}
