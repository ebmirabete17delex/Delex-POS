namespace Delex_POS.Domain.Entities;

public class Nationality : BaseAuditableEntity
{
    public string Name { get; init ;  } = string.Empty;
    public string? LocalName { get; private set; } = string.Empty; 
    public Nationality (string name, string? localName)
    {
        Name = name;
        LocalName = localName ?? string.Empty;
    }
    public void Update(string localName)
    {
        LocalName = localName ?? string.Empty;
    }
} 
