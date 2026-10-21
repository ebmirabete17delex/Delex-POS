namespace Delex_POS.Domain.Entities;

public class PsgcMunicipality : BaseEntity
{
    public string Code { get; private set; } = string.Empty;
    public string AreaName { get; private set; } = string.Empty;
    public string IslandRegion { get; private set; } = string.Empty;
    public string Version { get; private set; } = string.Empty;
    public string ProvinceCode { get; private set; } = string.Empty;

    public PsgcMunicipality(string code, string areaName, string islandRegion, string version, string provinceCode)
    {
        Code = code;
        AreaName = areaName;
        IslandRegion = islandRegion;
        Version = version;
        ProvinceCode = provinceCode;
    }
}
