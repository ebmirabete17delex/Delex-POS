namespace Delex_POS.Domain.Entities;

public class PsgcRegion : BaseEntity
{
    public string Code { get; private set; } = string.Empty;
    public string AreaName { get; private set; } = string.Empty;
    public string IslandRegion { get; private set; } = string.Empty;
    public string Version { get; private set; } = string.Empty;

    public PsgcRegion(string code, string areaName, string islandRegion, string version)
    {
        Code = code;
        AreaName = areaName;
        IslandRegion = islandRegion;
        Version = version;
    }
    public void Update(string areaName, string islandRegion, string version)
    {
        AreaName = areaName;
        IslandRegion = islandRegion;
        Version = version;
    }
}
