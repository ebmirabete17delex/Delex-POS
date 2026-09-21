using System.Text.Encodings.Web;
using System.Text.Json;

namespace Delex_POS.Application.Common.Extensions;

public static class JsonOptionsExtension
{
    public static readonly JsonSerializerOptions JsonCamelCase = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        AllowTrailingCommas = true
    };
    
    public static readonly JsonSerializerOptions UnsafeRelaxedJsonEscaping = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };
}
