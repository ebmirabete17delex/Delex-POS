using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using Delex_POS.Infrastructure.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Delex_POS.Infrastructure.Factories;
public class TokenFactory
{
    private readonly IConfiguration _configuration;

    public TokenFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateAccessToken(ApplicationUser user, int expiryMinutes)
    {
        var tokenHandler = new JsonWebTokenHandler();
        
        // Convert your secret key into bytes (Should be stored securely in appsettings.json or Key Vault)
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);

        // Define the user claims (Identity data embedded inside the token)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email?.ToString() ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Define the token specifications
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        // Create and serialize the token to a string
        return tokenHandler.CreateToken(tokenDescriptor);
    }


    public string GenerateRefreshToken()
    {
        // Allocate a buffer byte array (32 bytes = 256 bits of entropy)
        var randomNumber = new byte[32];
        
        // Fill the array with a cryptographically strong random sequence of values
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        
        // Convert the bytes to a URL-safe, clean string
        return Convert.ToBase64String(randomNumber);
    }
}