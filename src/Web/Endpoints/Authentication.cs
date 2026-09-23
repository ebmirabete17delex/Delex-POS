using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Models;
using Delex_POS.Infrastructure.Identity;

namespace Delex_POS.Web.Endpoints;

public class Authentication : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(Login, "login");
        groupBuilder.MapPost(Logout, "logout").RequireAuthorization();
    }

    [EndpointSummary("Log in")]
    [EndpointDescription("Authenticates a user and creates an authentication cookie.")]
    public static async Task<Results<Ok<TokenResponse>, UnauthorizedHttpResult>> Login(
        IIdentityService identityService,
        LoginRequest request)
    {
        var result = await identityService.LoginAsync(
            request.Email,
            request.Password,
            request.IsPersistent);

        return result is not null
            ? TypedResults.Ok(result)
            : TypedResults.Unauthorized();
    }

    [EndpointSummary("Log out")]
    [EndpointDescription("Logs out the current user by clearing the authentication cookie.")]
    public static async Task<Results<Ok, UnauthorizedHttpResult>> Logout(SignInManager<ApplicationUser> signInManager, [FromBody] object empty)
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return TypedResults.Ok();
        }

        return TypedResults.Unauthorized();
    }

}

public sealed record LoginRequest(
    string Email,
    string Password,
    bool IsPersistent = false);
