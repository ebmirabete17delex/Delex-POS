namespace Delex_POS.Application.Common.Models;

public record ActiveDirectoryUsersResponse(
    long? OdataCount,
    string? OdataNextLink,
    ActiveDirectoryUser[]? ActiveDirectoryUsers
);

public record ActiveDirectoryUser(
    string? Id,
    string? DisplayName,
    string? Mail,
    string? UserPrincipalName,
    string? GivenName,
    string? Surname
);
