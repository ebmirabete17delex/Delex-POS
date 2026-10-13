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
        groupBuilder.MapPost(Activate, "activate");
        groupBuilder.MapPost(Logout, "logout").RequireAuthorization();
    }

    [EndpointSummary("Log in")]
    [EndpointDescription("Authenticates a user and creates an authentication cookie.")]
    public static async Task<Results<Ok<TokenResponse>, UnauthorizedHttpResult>> Login(
        IIdentityService identityService,
        LoginRequest request)
    {
        var result = await identityService.LoginAsync(
            request.UserId,
            request.Password,
            request.IsPersistent);

        return result is not null
            ? TypedResults.Ok(result)
            : TypedResults.Unauthorized();
    }

    [EndpointSummary("Activate an account")]
    [EndpointDescription("Sets a new user's six-digit PIN and activates their account.")]
    public static async Task<Results<Ok<TokenResponse>, BadRequest<string>, UnauthorizedHttpResult>> Activate(
        IIdentityService identityService,
        ActivationRequest request)
    {
        if (string.IsNullOrEmpty(request.Pin) ||
            request.Pin.Length != 6 ||
            request.Pin.Any(character => character < '0' || character > '9'))
        {
            return TypedResults.BadRequest("PIN must be exactly six digits.");
        }

        var result = await identityService.ActivateAsync(
            request.UserId,
            request.Password,
            request.Pin);

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
    string UserId,
    string Password,
    bool IsPersistent = false);

public sealed record ActivationRequest(
    string UserId,
    string Password,
    string Pin);
