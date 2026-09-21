
namespace Delex_POS.Domain.Entities.RBAC;
public class Branch : BaseAuditableEntity
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Location { get; private set; }
    public string? Email { get; private set; } = string.Empty;
    public string? ContactNumber { get; private set; } = string.Empty;

    public Branch(string code, string name, string? location, string? email, string? contactNumber)
    {
        Code = code;
        Name = name;
        Location = location ?? string.Empty;
        Email = email ?? string.Empty;
        ContactNumber = contactNumber ?? string.Empty;
    }

    public void Update(string name, string? location, string? email, string? contactNumber)
    {
        Name = name;
        Location = location ?? string.Empty;
        Email = email ?? string.Empty;
        ContactNumber = contactNumber ?? string.Empty;
    }
}

