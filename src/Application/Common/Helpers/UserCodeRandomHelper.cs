using System.Security.Cryptography;

namespace Delex_POS.Application.Common.Helpers;

public static class UserCodeRandomHelper
{
    /// <summary>
    /// Generates a cryptographically secure random 6-digit numeric code as a string (padded with leading zeros).
    /// Example: "042391".
    /// </summary>
    public static string GenerateSixDigitUserCode()
    {
        // Generate an integer in range [0, 1_000_000) and format as 6 digits with leading zeros
        int value = RandomNumberGenerator.GetInt32(0, 1_000_000);
        return value.ToString("D6");
    }
}
