namespace Delex_POS.Domain.Entities;

public class PsgcBarangay : BaseEntity
{
    public string Code { get; private set; } = string.Empty;
    public string AreaName { get; private set; } = string.Empty;
    public string IslandRegion { get; private set; } = string.Empty;
    public string Version { get; private set; } = string.Empty;
    public string MunicipalityCode { get; private set; } = string.Empty;
    public PsgcBarangay(string code, string areaName, string islandRegion, string version, string municipalityCode)
    {
        Code = code;
        AreaName = areaName;
        IslandRegion = islandRegion;
        Version = version;
        MunicipalityCode = municipalityCode;
    }
}
