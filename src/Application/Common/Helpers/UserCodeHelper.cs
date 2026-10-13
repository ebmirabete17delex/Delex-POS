using System.Globalization;
using System.Linq;

namespace Delex_POS.Application.Common.Helpers;

public static class UserCodeHelper
{
    /// <summary>
    /// Computes the next available 6-digit numeric user code from a collection of existing codes.
    /// Only strings that represent integer values between 0 and 999999 (inclusive) are considered.
    /// Returns the next value formatted as a 6-digit string (D6). If no valid codes are present, returns "000001".
    /// Throws InvalidOperationException when the next value would exceed 999999.
    /// </summary>
    public static string GetNextSixDigitFromLastCode(string lastCode)
    {
        if (lastCode is null) throw new ArgumentNullException(nameof(lastCode));
        if (!int.TryParse(lastCode, out int max)) throw new ArgumentNullException(nameof(lastCode));

        // Parse valid numeric codes and find the maximum
        int next = max + 1;
        if (next > 999_999)
            throw new InvalidOperationException("No available 6-digit user codes remain.");

        return next.ToString("D6", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Computes the next available 6-digit numeric user code from a collection of existing codes.
    /// Only strings that represent integer values between 0 and 999999 (inclusive) are considered.
    /// Returns the next value formatted as a 6-digit string (D6). If no valid codes are present, returns "000001".
    /// Throws InvalidOperationException when the next value would exceed 999999.
    /// </summary>
    public static string GetNextSixDigitFromCodes(IEnumerable<string?> existingCodes)
    {
        if (existingCodes is null) throw new ArgumentNullException(nameof(existingCodes));

        // Parse valid numeric codes and find the maximum
        int max = existingCodes
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s!.Trim())
            .Select(s =>
            {
                // Accept purely-digit strings or any integer parseable value up to 999999
                if (s.All(char.IsDigit) && int.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture, out var v))
                    return (int?)v;

                // Try a lenient parse for strings that might include leading/trailing whitespace or other formatting
                if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v2))
                    return v2;

                return (int?)null;
            })
            .Where(v => v.HasValue && v.Value >= 0 && v.Value <= 999_999)
            .Select(v => v!.Value)
            .DefaultIfEmpty(0)
            .Max();

        int next = max + 1;
        if (next > 999_999)
            throw new InvalidOperationException("No available 6-digit user codes remain.");

        return next.ToString("D6", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Async helper that accepts a delegate to fetch existing user codes (for example, from the AspNetUsers table)
    /// and returns the next 6-digit code.
    /// </summary>
    public static async Task<string> GetNextSixDigitFromCodesAsync(Func<Task<IEnumerable<string?>>> fetchExistingCodes)
    {
        if (fetchExistingCodes is null) throw new ArgumentNullException(nameof(fetchExistingCodes));

        var codes = await fetchExistingCodes().ConfigureAwait(false);
        return GetNextSixDigitFromCodes(codes);
    }
}
