using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace Delex_POS.Infrastructure.Jwt;

/// <summary>
/// Provides JWKS (JSON Web Key Set) functionality for exposing public keys.
/// Useful for RS256 (asymmetric) JWT signing where multiple services need to validate tokens.
/// For HS256 (symmetric), this is not needed as the secret is only server-side.
/// </summary>
public class JwksProvider
{
    private readonly IConfiguration _configuration;

    public JwksProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Builds a JWKS response with the current signing public key.
    /// This endpoint should be exposed at /.well-known/jwks.json for external validation.
    /// </summary>
    public object BuildJwks(RSA rsa, string kid = "default")
    {
        var pub = rsa.ExportParameters(false);
        var jwk = new Dictionary<string, object?>
        {
            ["kty"] = "RSA",
            ["kid"] = kid,
            ["use"] = "sig",
            ["alg"] = "RS256",
            ["n"] = Base64UrlEncoder.Encode(pub.Modulus!),
            ["e"] = Base64UrlEncoder.Encode(pub.Exponent!)
        };
        return new { keys = new[] { jwk } };
    }

    /// <summary>
    /// Loads an RSA key from an X509 certificate for asymmetric signing (RS256).
    /// For production, store the certificate in Azure Key Vault or similar.
    /// </summary>
    public static RSA LoadRsaFromCertificate(string certificatePath, string? password = null)
    {
        var cert = X509CertificateLoader.LoadCertificateFromFile(certificatePath);
        //var cert = new X509Certificate2(certificatePath, password);
        return cert.GetRSAPrivateKey() 
            ?? throw new InvalidOperationException("Certificate has no RSA private key.");
    }

    /// <summary>
    /// Loads an RSA key from the certificate store by thumbprint.
    /// This is the recommended approach for production environments.
    /// </summary>
    public static RSA LoadRsaFromCertificateStore(string thumbprint)
    {
        using var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        store.Open(OpenFlags.ReadOnly);
        var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false);

        if (certs.Count == 0)
        {
            throw new InvalidOperationException($"Certificate with thumbprint '{thumbprint}' not found.");
        }

        var cert = certs[0];
        return cert.GetRSAPrivateKey() 
            ?? throw new InvalidOperationException("Certificate has no RSA private key.");
    }
}
