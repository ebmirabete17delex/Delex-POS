namespace Delex_POS.Domain.Entities;

public class PsgcMunicipality : PsgcRegion
{
    public PsgcMunicipality(string code,
        string areaName,
        string islandRegion,
        string version) : base(code, areaName, islandRegion, version)
    { }

}
