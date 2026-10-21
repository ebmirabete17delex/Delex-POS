
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Delex_POS.Infrastructure.Jwt;

public static class CertificateStore
{
    private static X509Certificate2? _developmentCertificate;
    private static RSA? _developmentRsa;

    // ---- Load X509 certificate from store or create a development one ----
    public static X509Certificate2 GetCertificate(string thumbprint)
    {
        if (string.IsNullOrWhiteSpace(thumbprint))
            throw new InvalidOperationException("Certificate thumbprint not configured.");

        // Try to find certificate in LocalMachine store
        using var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        store.Open(OpenFlags.ReadOnly);
        var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false);
        if (certs.Count > 0)
            return certs[0];

        // Try to find certificate in CurrentUser store
        store.Close();
        using var store2 = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store2.Open(OpenFlags.ReadOnly);
        var certs2 = store2.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false);
        if (certs2.Count > 0)
            return certs2[0];

        // Fallback: Create a self-signed certificate for development/build scenarios
        return GetOrCreateDevelopmentCertificate();
    }

    private static X509Certificate2 GetOrCreateDevelopmentCertificate()
    {
        if (_developmentCertificate != null && _developmentRsa != null)
        {
            // Add the RSA key to the certificate for this retrieval
            _developmentCertificate = _developmentCertificate.CopyWithPrivateKey(_developmentRsa);
            return _developmentCertificate;
        }

        _developmentRsa = RSA.Create(2048);
        var request = new CertificateRequest("cn=DelexPOS-Dev", _developmentRsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        request.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(false, false, 0, false));
        request.CertificateExtensions.Add(
            new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, false));

        using var cert = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(1));
        _developmentCertificate = cert.CopyWithPrivateKey(_developmentRsa);

        return _developmentCertificate;
    }
}

