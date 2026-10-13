namespace Delex_POS.Domain.Entities;

public class PsgcRegion : BaseAuditableEntity
{
    public string PsgcCode { get; private set; } = string.Empty;
    public string PsgcAreaName { get; private set; } = string.Empty;
    public string PsgcIslandRegion { get; private set; } = string.Empty;
    public string PsgcVersion { get; private set; } = string.Empty;

    public PsgcRegion(string code, string areaName, string islangRegion, string version)
    {
        PsgcCode = code;
        PsgcAreaName = areaName;
        PsgcIslandRegion = islangRegion;
        PsgcVersion = version;
    }
    public void Update(string areaName, string islangRegion, string version)
    {
        PsgcAreaName = areaName;
        PsgcIslandRegion = islangRegion;
        PsgcVersion = version;
    }
}
