using Delex_POS.Application.Users.Commands.CreateUser;
using Delex_POS.Application.Users.Commands.UpdateUser;
using Delex_POS.Application.Users.Commands.DeleteUser;
using Delex_POS.Application.Users.Commands.AssignRole;
using Delex_POS.Application.Users.Commands.DismissRole;
using Delex_POS.Application.Users.Commands.AddUserAccess;
using Delex_POS.Application.Users.Commands.RemoveUserAccess;
using Delex_POS.Application.Users.Queries.GetUser;
using Delex_POS.Application.Users.Queries.GetUsers;
using Delex_POS.Application.Users.Queries.GetUserRoles;
using Delex_POS.Application.Users.Queries.GetUserAccesses;
using Delex_POS.Application.Users.Queries.UserDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Delex_POS.Application.Common.Models;

namespace Delex_POS.Web.Endpoints;

public class Users : IEndpointGroup
{
    public static string? RoutePrefix => "/api/Users";

    public static void Map(RouteGroupBuilder groupBuilder)
    {
        // groupBuilder.MapIdentityApi<ApplicationUser>();

        groupBuilder.MapGet(GetUsers);

        groupBuilder.MapGet(GetUser, "{userId}");
        groupBuilder.MapPost(CreateUser);
        groupBuilder.MapPut(UpdateUser, "{userId}");
        groupBuilder.MapDelete(DeleteUser, "{userId}");

        groupBuilder.MapGet(GetUserAccess, "{userId}/accesses");
        groupBuilder.MapPost(AddUserAccess, "{userId}/accesses/{accessId}");
        groupBuilder.MapDelete(RemoveUserAccess, "{userId}/accesses/{accessId}");

        groupBuilder.MapGet(GetUserRoles, "{userId}/roles");
        groupBuilder.MapPost(AssignUserRole, "{userId}/roles/{roleId}");
        groupBuilder.MapDelete(DismissUserRole, "{userId}/roles/{roleId}");       

    }

    [EndpointSummary("Get all Users")]
    [EndpointDescription("Retrieves all users along with their items.")]
    public static async Task<Ok<PaginatedList<ApplicationUserDto>>> GetUsers(ISender sender, [AsParameters] GetUsersQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get User")]
    [EndpointDescription("Retrieves user along with their items.")]
    public static async Task<Ok<ApplicationUserDto>> GetUser(ISender sender, string userId)
    {
        var vm = await sender.Send(new GetUserQuery { Id = userId });

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
    public static async Task<Results<NoContent, BadRequest>> UpdateUser(ISender sender, string userId, UpdateUserCommand command)
    {
        if (userId != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a user")]
    [EndpointDescription("Deletes the user with the specified ID.")]
    public static async Task<NoContent> DeleteUser(ISender sender, string userId)
    {
        await sender.Send(new DeleteUserCommand(userId));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get User Roles")]
    [EndpointDescription("Retrieves user roles along with their items.")]
    public static async Task<Ok<List<UserRoleDto>>> GetUserRoles(ISender sender, string userId)
    {
        var vm = await sender.Send(new GetUserRolesQuery { Id = userId });

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Assign role to user")]
    [EndpointDescription("Assigns a role to user using the provided details and returns the ID of the created item.")]
    public static async Task<Created<string>> AssignUserRole(ISender sender, string userId, string roleId)
    {
        await sender.Send(new AssignRoleCommand
        {
            UserId = userId,
            Role = roleId
        });

        return TypedResults.Created($"/api/Users/{userId}/roles/{roleId}", roleId);
    }

    [EndpointSummary("Dismiss a Role from a user")]
    [EndpointDescription("Dismiss a role from a user with the specified IDs.")]
    public static async Task<NoContent> DismissUserRole(ISender sender, string userId, string roleId)
    {
        await sender.Send(new DismissRoleCommand
        {
            Role = roleId,
            UserId = userId
        });

        return TypedResults.NoContent();
    }

[EndpointSummary("Get all user access")]
    [EndpointDescription("Retrieves all user access.")]
    public static async Task<Ok<PaginatedList<UserAccessDto>>> GetUserAccess(ISender sender, string userId, [AsParameters] GetUserAccessQuery query)
    {
        var vm = await sender.Send(query with { UserId = userId });

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Grant a user access")]
    [EndpointDescription("Adds a user access using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> AddUserAccess(ISender sender, string userId, int accessId)
    {
        var result = await sender.Send(new AddUserAccessCommand
        {
            UserId = userId,
            AccessId = accessId
        });

        return TypedResults.Created($"/api/Users/{userId}/accesses/{result}", result);
    }

    [EndpointSummary("Revoke a User Access")]
    [EndpointDescription("Remove a user access with the specified IDs.")]
    public static async Task<NoContent> RemoveUserAccess(ISender sender, string userId, int accessId)
    {
        await sender.Send(new RemoveUserAccessCommand
        {
            UserId = userId,
            AccessId = accessId
        });

        return TypedResults.NoContent();
    }}
