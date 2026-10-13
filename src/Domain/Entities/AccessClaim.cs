namespace Delex_POS.Domain.Entities;
public class AccessClaim : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public string Feature { get; private set; } = string.Empty;
    public string BackendUrl { get; private set; } = string.Empty;
    public string FrontendUrl { get; private set; } = string.Empty;

    public AccessClaim(string name, string feature)
    {
        Name = name;
        Feature = feature;
        BackendUrl = $"/api/{feature}";
        FrontendUrl = $"/{feature}";
    }

    public void Update(string feature)
    {
        Feature = feature;
        BackendUrl = $"/api/{feature}";
        FrontendUrl = $"/{feature}";
    }
}
