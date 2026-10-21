namespace Delex_POS.Domain.Entities;

public class PsgcProvince : BaseEntity
{
    public string Code { get; private set; } = string.Empty;
    public string AreaName { get; private set; } = string.Empty;
    public string IslandRegion { get; private set; } = string.Empty;
    public string Version { get; private set; } = string.Empty;
    public string RegionCode { get; private set; } = string.Empty;
    public PsgcProvince(string code, string areaName, string islandRegion, string version, string regionCode)
    {
        Code = code;
        AreaName = areaName;
        IslandRegion = islandRegion;
        Version = version;
        RegionCode = regionCode;
    }
}
