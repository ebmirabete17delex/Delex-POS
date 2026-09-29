using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class CancelReason : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public bool IsAllowUserToEnterMemo { get; private set; }
    public string? Memo { get; private set; } = string.Empty;

    public CancelReason(string name, bool isActive, bool isAllowUserToEnterMemo, string memo)
    {
        Name = name;
        IsActive = isActive;
        IsAllowUserToEnterMemo = isAllowUserToEnterMemo;
        Memo = memo;
    }

    public void Update(string memo)
    {  
        Memo = memo; 
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void AllowUserToEnterMemo() 
    {
        IsAllowUserToEnterMemo = true;
    }

    public void DisallowUserToEnterMemo()
    {
        IsAllowUserToEnterMemo = false;
    }
}
