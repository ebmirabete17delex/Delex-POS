namespace Delex_POS.Application.Common.Models;

public class TokenResponse
{
    public string access_token { get; set; } = string.Empty;
    public string token_type { get; set; } = string.Empty;
    public int expires_in { get; set; }
}

public class OboTokenErrorBody
{
    public string type { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public int status { get; set; }
    public Dictionary<string, List<string>> errors { get; set; } = new();
    public string traceId { get; set; } = string.Empty;
}
