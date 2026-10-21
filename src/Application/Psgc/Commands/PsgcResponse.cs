namespace Delex_POS.Application.Psgc.Commands;

public class PsgcResponse
{
    public int Count { get; init; }
    public string Next { get; init; } = string.Empty;
    public string Previous { get; init; } = string.Empty;
    public IEnumerable<PsgcResponseResult> Results { get; set; } = [];
}

public class PsgcResponseResult
{
    public string Code { get; set; } = string.Empty;
    public string Area_Name { get; set; } = string.Empty;
    public string Island_Region { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}
