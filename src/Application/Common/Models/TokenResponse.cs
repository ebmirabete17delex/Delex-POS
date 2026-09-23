namespace Delex_POS.Application.Common.Models;

public class TokenResponse
{
    public string? TokenType { get; init; }
    public string AccessToken { get; init; } = string.Empty;
    public int ExpiresIn { get; init; }
    public string RefreshToken { get; init; } = string.Empty;
}

public class OboTokenErrorBody
{
    public string type { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public int status { get; set; }
    public Dictionary<string, List<string>> errors { get; set; } = new();
    public string traceId { get; set; } = string.Empty;
}
