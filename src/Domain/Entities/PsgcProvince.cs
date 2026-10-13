namespace Delex_POS.Domain.Entities;

public class PsgcProvince : PsgcRegion
{
    public PsgcProvince(
        string code, 
        string areaName,
        string  islandRegion, 
        string version ) : base (code, areaName, islandRegion, version)
    {

    }
     
}
