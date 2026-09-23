using Delex_POS.Application.Users.Commands.CreateUser;
using Delex_POS.Application.Users.Commands.UpdateUser;
using Delex_POS.Application.Users.Commands.DeleteUser;
using Delex_POS.Application.Users.Commands.AssignRole;
using Delex_POS.Application.Users.Queries.GetUser;
using Delex_POS.Application.Users.Queries.GetUsers;
using Delex_POS.Infrastructure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Delex_POS.Application.Common.Models;
using System.Text.RegularExpressions;

namespace Delex_POS.Web.Endpoints;

public class Users : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        // groupBuilder.MapIdentityApi<ApplicationUser>();

        groupBuilder.MapGet(GetUsers).RequireAuthorization();

        groupBuilder.MapGet(GetUser, "{id}")
            .Produces<NotFound>()
            .Produces<Ok>();

        groupBuilder.MapPost(CreateUser);

        groupBuilder.MapPut(UpdateUser, "{id}");
        
        groupBuilder.MapDelete(DeleteUser, "{id}");

        // groupBuilder.MapPost(Logout, "logout").RequireAuthorization();
    }

    // [EndpointSummary("Log out")]
    // [EndpointDescription("Logs out the current user by clearing the authentication cookie.")]
    // public static async Task<Results<Ok, UnauthorizedHttpResult>> Logout(SignInManager<ApplicationUser> signInManager, [FromBody] object empty)
    // {
    //     if (empty != null)
    //     {
    //         await signInManager.SignOutAsync();
    //         return TypedResults.Ok();
    //     }

    //     return TypedResults.Unauthorized();
    // }

    [EndpointSummary("Get all Users")]
    [EndpointDescription("Retrieves all users along with their items.")]
    public static async Task<Ok<PaginatedList<ApplicationUserDto>>> GetUsers(ISender sender, [AsParameters] GetUsersQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get User")]
    [EndpointDescription("Retrieves user along with their items.")]
    public static async Task<Ok<ApplicationUserDto>> GetUser(ISender sender, [AsParameters] GetUserQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Create a new User")]
    [EndpointDescription("Creates a new User using the provided details and returns the ID of the created list.")]
    public static async Task<Created<string>> CreateUser(ISender sender, CreateUserCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Users)}/{id}", id);
    }

    [EndpointSummary("Update a User")]
    [EndpointDescription("Updates the specified User. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateUser(ISender sender, string id, UpdateUserCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a user")]
    [EndpointDescription("Deletes the user with the specified ID.")]
    public static async Task<NoContent> DeleteUser(ISender sender, string id)
    {
        await sender.Send(new DeleteUserCommand(id));

        return TypedResults.NoContent();
    }
}
