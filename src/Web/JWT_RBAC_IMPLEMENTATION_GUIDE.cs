// JWT with Role-Based Access Control (RBAC) Implementation Guide
// ================================================================

/*
This document explains how to use the JWT implementation with RBAC in your application.

1. CONFIGURATION
================

Add this to your appsettings.json:

{
  "Jwt": {
    "SecretKey": "your-256-bit-secret-key-here-minimum-32-characters",
    "Issuer": "https://your-domain.com",
    "Audience": "your-app-name"
  }
}

For production, store the SecretKey in Azure Key Vault or AWS Secrets Manager.

2. AUTHENTICATION FLOW
======================

a) User Login:
   POST /api/auth/login
   Body: { "userId": "john@example.com", "password": "password123" }

   Response: 
   {
     "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
     "tokenType": "Bearer",
     "expiresIn": 3600,
     "refreshToken": "base64-encoded-refresh-token",
     "isActive": true
   }

b) Using Access Token:
   Include in API requests: Authorization: Bearer {accessToken}

c) Token Refresh:
   POST /api/auth/refresh
   Cookie: refresh_token=base64-token
   (The refresh token is stored as HttpOnly cookie, so it's automatically sent)

3. ROLE-BASED ACCESS CONTROL (RBAC)
====================================

Roles are embedded in the JWT as claims. Available roles:
- Administrator: Full system access
- Manager: Department/location management
- Staff: Data entry and operations
- User: Read-only access

4. USING AUTHORIZATION POLICIES
=================================

In your endpoints, use the [Authorize] attribute with policy names:

// Allow only Administrators
[Authorize(Policy = "Administrator")]
public static async Task<Ok<Data>> AdminEndpoint() { ... }

// Allow Managers and Administrators
[Authorize(Policy = "AdministratorOrManager")]
public static async Task<Ok<Data>> ManagerEndpoint() { ... }

// Allow authenticated users with any role
[Authorize(Policy = "AnyAuthenticated")]
public static async Task<Ok<Data>> PublicEndpoint() { ... }

5. CHECKING ROLES PROGRAMMATICALLY
====================================

In your endpoint handlers:

public static async Task<Ok> MyEndpoint(System.Security.Claims.ClaimsPrincipal user)
{
    // Get current user ID
    var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

    // Check if user has specific role
    bool isAdmin = user.IsInRole(Domain.Constants.Roles.Administrator);
    bool isManager = user.IsInRole("Manager");

    // Get user email
    var email = user.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

    // Get user name
    var name = user.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

    // Get user code
    var userCode = user.FindFirst("user_code")?.Value;

    return TypedResults.Ok();
}

6. TOKEN STRUCTURE
==================

JWT Access Token contains these claims:
- sub (subject): User ID
- iss (issuer): Token issuer (your domain)
- aud (audience): App name
- email: User email
- name: User name
- user_code: Custom user code
- role: User roles (multiple claim entries for multiple roles)
- jti: JWT ID (unique token identifier)
- exp: Expiration time
- iat: Issued at
- nbf: Not before

Example decoded JWT:
{
  "sub": "user-guid-123",
  "iss": "https://your-domain.com",
  "aud": "your-app-name",
  "email": "john@example.com",
  "name": "John Doe",
  "user_code": "USR001",
  "role": ["Manager", "Staff"],
  "jti": "abc-123-def",
  "exp": 1234567890,
  "iat": 1234567800,
  "nbf": 1234567800
}

7. REFRESH TOKEN ROTATION
==========================

- Refresh tokens are cryptographically random, 256-bit strings
- Stored as HttpOnly cookies (immune to XSS)
- Hashed and stored in database for security
- Rotated on every use (old token removed, new token issued)
- Can be revoked by marking as revoked in database
- Expires after 30 days

8. SECURITY BEST PRACTICES
===========================

✓ Store SecretKey in secure vaults (Azure Key Vault, AWS KMS)
✓ Use HTTPS in production
✓ Set refresh token as HttpOnly cookie (prevents XSS)
✓ Implement CSRF protection
✓ Monitor for refresh token reuse (indicates account compromise)
✓ Implement rate limiting on login endpoint
✓ Implement account lockout after failed login attempts
✓ Log all authentication events
✓ Rotate secrets periodically
✓ Use short-lived access tokens (1 hour recommended)
✓ Use long-lived refresh tokens (30 days recommended)

9. CUSTOM ROLE CLAIMS
======================

To add custom claims to the JWT, modify TokenFactory.GenerateAccessTokenAsync():

var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id),
    new Claim(ClaimTypes.Email, user.Email ?? ""),
    // Add custom claims here
    new Claim("department", user.Department),
    new Claim("location", user.Location),
    new Claim("permissionLevel", user.PermissionLevel.ToString()),
    // ... more claims
};

Then access them in endpoints:
var department = user.FindFirst("department")?.Value;
var location = user.FindFirst("location")?.Value;

10. HANDLING TOKEN EXPIRATION ON CLIENT
========================================

Frontend clients should:
1. Store access token in memory (not localStorage for XSS protection)
2. Store refresh token in HttpOnly cookie
3. When API returns 401 Unauthorized:
   a. Call /api/auth/refresh to get new access token
   b. If refresh fails, redirect to login
4. Implement token refresh logic before token expiration (optional)
5. Clear all tokens on logout

Example TypeScript/React pattern:
async function makeAuthenticatedRequest(url, options) {
    let response = await fetch(url, {
        ...options,
        headers: {
            ...options.headers,
            'Authorization': `Bearer ${accessToken}`
        }
    });

    if (response.status === 401) {
        // Token expired, try refresh
        const refreshResponse = await fetch('/api/auth/refresh', {
            method: 'POST',
            credentials: 'include' // Include cookies
        });

        if (refreshResponse.ok) {
            const data = await refreshResponse.json();
            accessToken = data.accessToken;

            // Retry the original request
            response = await fetch(url, {
                ...options,
                headers: {
                    ...options.headers,
                    'Authorization': `Bearer ${accessToken}`
                }
            });
        } else {
            // Refresh failed, redirect to login
            window.location.href = '/login';
        }
    }

    return response;
}
*/

// Examples for updating existing endpoints with RBAC
// ====================================================

/*
BEFORE (Without authorization):
public class Product : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetProducts);
        groupBuilder.MapPost(CreateProduct);
        groupBuilder.MapPut(UpdateProduct, "{id}");
        groupBuilder.MapDelete(DeleteProduct, "{id}");
    }
}

AFTER (With RBAC):
public class Product : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        // Anyone authenticated can read products
        groupBuilder.MapGet(GetProducts).RequireAuthorization("AnyAuthenticated");

        // Only managers and above can create products
        groupBuilder.MapPost(CreateProduct).RequireAuthorization("AdministratorOrManager");

        // Only managers and above can update products
        groupBuilder.MapPut(UpdateProduct, "{id}").RequireAuthorization("AdministratorOrManager");

        // Only administrators can delete products
        groupBuilder.MapDelete(DeleteProduct, "{id}").RequireAuthorization("Administrator");
    }

    public static async Task<Ok<List<ProductDto>>> GetProducts()
    {
        // Any authenticated user can reach here
        return TypedResults.Ok(await _service.GetAllAsync());
    }

    public static async Task<Created<int>> CreateProduct(CreateProductCommand command)
    {
        // Only AdministratorOrManager can reach here
        var id = await sender.Send(command);
        return TypedResults.Created($"/products/{id}", id);
    }
}
*/

namespace Delex_POS.Web;

/// <summary>
/// JWT and RBAC implementation guide.
/// This file serves as documentation and can be deleted after implementation.
/// </summary>
public class JwtRbacImplementationGuide
{
    // This class is for documentation only and will be removed in production
}
