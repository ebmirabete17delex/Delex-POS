using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Tls;
namespace Delex_POS.Infrastructure.Jwt;

public static class JwtHelper
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

    public static (RsaSecurityKey? signingkey, string? kid) BuildRsaKeyandKId(X509Certificate2? cert)
    {
        var rsa = cert!.GetRSAPrivateKey() ?? throw new InvalidOperationException("Certificate has no RSA private key.");
        var signingKey = new RsaSecurityKey(rsa) { KeyId = cert!.Thumbprint!.Replace(" ", "").ToLowerInvariant() };
        var kid = signingKey.KeyId;
        return (signingKey, kid);
    }
    public static string CreateAccessToken(string userId, IEnumerable<System.Security.Claims.Claim>? extra = null)
    {
        var accessTokenLifetime = TimeSpan.FromMinutes(5);
        string? issuer = ConfigurationManager.AppSettings["Jwt:Issuer"] ?? "";
        string? audience = ConfigurationManager.AppSettings["Jwt:Audience"] ?? "";
        string? certThumbprint = ConfigurationManager.AppSettings["Cert:Thumbprint"] ?? "<YOUR_CERT_THUMBPRINT>";

        var cert = CertificateStore.GetCertificate(certThumbprint);
        var signKeyKid = BuildRsaKeyandKId(cert);

        var now = DateTime.UtcNow;
        var claims = new List<System.Security.Claims.Claim>
    {
        new("sub", userId),
        new("iss", issuer),
        new("aud", audience)
    };
        if (extra != null) claims.AddRange(extra);

        var creds = new SigningCredentials(signKeyKid.signingkey, SecurityAlgorithms.RsaSha256);
        var jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: now,
            expires: now.Add(accessTokenLifetime),
            signingCredentials: creds
        );
        jwt.Header["kid"] = signKeyKid.kid;
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
