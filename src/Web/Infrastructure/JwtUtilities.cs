using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Delex_POS.Web.Infrastructure;

public static class JwtUtilities
{
    public static string HashToken(string token)
    {
        using var sha = SHA256.Create();
        return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(token)));
    }
    public static string MakeRandomToken(int bytes = 64)
    {
        var b = RandomNumberGenerator.GetBytes(bytes);
        return Base64UrlEncoder.Encode(b);
    }
}
