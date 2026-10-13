namespace Delex_POS.Domain.Entities;

public class PsgcBarangay : PsgcRegion
{
    public PsgcBarangay(string code,
        string areaName,
        string islandRegion,
        string version) : base(code, areaName, islandRegion, version)
    { }
}
