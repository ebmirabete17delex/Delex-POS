using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class Region : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public string? Memo { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public Region (string name, string memo)
    {
        Name = name;
        Memo = memo;
        IsActive = true;
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
