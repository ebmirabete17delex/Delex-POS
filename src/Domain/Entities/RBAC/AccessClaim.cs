namespace Delex_POS.Domain.Entities.RBAC;
public class AccessClaim : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public string Feature { get; private set; } = string.Empty;
    public string BackendUrl { get; private set; } = string.Empty;
    public string FrontendUrl { get; private set; } = string.Empty;

    public AccessClaim(string name, string feature, string backendUrl, string frontendUrl)
    {
        Name = name;
        Feature = feature;
        BackendUrl = backendUrl;
        FrontendUrl = frontendUrl;
    }
}