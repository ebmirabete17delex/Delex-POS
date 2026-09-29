using System.Xml.Linq;

namespace Delex_POS.Domain.Entities;

public class PriceLevel: BaseAuditableEntity
{
    public string Name { get; init;  } = string.Empty;
    public bool IsActive { get; private set; } 
    public string? Memo { get; init; }
    public PriceLevel(string name, bool isActive, string? memo)
    {
        Name = name;
        IsActive = isActive;
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
}
