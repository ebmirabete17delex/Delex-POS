using Delex_POS.Application.Employees.Commands.CreateEmployee;
using Delex_POS.Application.Employees.Commands.UpdateEmployee;
using Delex_POS.Application.Employees.Commands.DeleteEmployee;
using Delex_POS.Application.Employees.Commands.AssignRole;
using Delex_POS.Application.Employees.Commands.DismissRole;
using Delex_POS.Application.Employees.Commands.AddUserAccess;
using Delex_POS.Application.Employees.Commands.RemoveUserAccess;
using Delex_POS.Application.Employees.Commands.UpdateUserAccess;
using Delex_POS.Application.Employees.Commands.CreateUserPermission;
using Delex_POS.Application.Employees.Commands.DeleteUserPermission;
using Delex_POS.Application.Employees.Queries.GetEmployee;
using Delex_POS.Application.Employees.Queries.GetEmployees;
using Delex_POS.Application.Employees.Queries.GetUserRoles;
using Microsoft.AspNetCore.Http.HttpResults;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Employees.Queries.GetUserAccess;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;
using Delex_POS.Application.Employees.Queries.GetUserPermissions;
using Delex_POS.Application.Employees.Queries.GetAccessPermissions;
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
        groupBuilder.MapPut(UpdateUserAccess, "{userId}/accesses/{accessId}");
        groupBuilder.MapDelete(RemoveUserAccess, "{userId}/accesses/{accessId}");

        groupBuilder.MapGet(GetUserRoles, "{userId}/roles");
        groupBuilder.MapPost(AssignUserRole, "{userId}/roles/{roleId}");
        groupBuilder.MapDelete(DismissUserRole, "{userId}/roles/{roleId}");

        groupBuilder.MapGet(GetUserPermissions, "{userId}/permission");
        groupBuilder.MapGet(GetUserAccessPermissions, "{userId}/accesspermission");
        groupBuilder.MapPost(CreateUserPermission, "{userId}/permission/");
        groupBuilder.MapDelete(DeleteUserPermission, "{userId}/permission/{id}");

    }

    [EndpointSummary("Get all Users")]
    [EndpointDescription("Retrieves all users along with their items.")]
    public static async Task<Ok<PaginatedList<EmployeeListDto>>> GetUsers(ISender sender, [AsParameters] GetEmployeesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get User")]
    [EndpointDescription("Retrieves user along with their items.")]
    public static async Task<Ok<EmployeeDto>> GetUser(ISender sender, int userId)
    {
        var vm = await sender.Send(new GetEmployeeQuery(Id: userId));

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Create a new User")]
    [EndpointDescription("Creates a new User using the provided details and returns the ID of the created list.")]
    public static async Task<Created<int>> CreateUser(ISender sender, CreateEmployeeCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/users/{id}", id);
    }

    [EndpointSummary("Update a User")]
    [EndpointDescription("Updates the specified User. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateUser(ISender sender, int userId, UpdateEmployeeCommand command)
    {
        if (userId != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a user")]
    [EndpointDescription("Deletes the user with the specified ID.")]
    public static async Task<NoContent> DeleteUser(ISender sender, int userId)
    {
        await sender.Send(new DeleteEmployeeCommand(userId));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get User Roles")]
    [EndpointDescription("Retrieves user roles along with their items.")]
    public static async Task<Ok<List<UserRoleDto>>> GetUserRoles(ISender sender, int userId)
    {
        var vm = await sender.Send(new GetUserRolesQuery { Id = userId });

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Assign role to user")]
    [EndpointDescription("Assigns a role to user using the provided details and returns the ID of the created item.")]
    public static async Task<Created<string>> AssignUserRole(ISender sender, int userId, string roleId)
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
    public static async Task<NoContent> DismissUserRole(ISender sender, int userId, string roleId)
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
    public static async Task<Ok<List<UserAccessDto>>> GetUserAccess(ISender sender, int userId)
    {
        var vm = await sender.Send(new GetUserAccessQuery { Id = userId });

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Grant a user access")]
    [EndpointDescription("Adds a user access using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> AddUserAccess(ISender sender, int userId, int accessId)
    {
        var result = await sender.Send(new AddUserAccessCommand
        {
            UserId = userId,
            AccessId = accessId
        });

        return TypedResults.Created($"/api/Users/{userId}/accesses/{result}", result);
    }

    [EndpointSummary("Update a user's access permissions")]
    [EndpointDescription("Updates the permissions and active status for a user's access claim.")]
    public static async Task<NoContent> UpdateUserAccess(
        ISender sender,
        int userId,
        int accessId,
        UpdateUserAccessCommand command)
    {
        command.UserId = userId;
        command.AccessId = accessId;
        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Revoke a User Access")]
    [EndpointDescription("Remove a user access with the specified IDs.")]
    public static async Task<NoContent> RemoveUserAccess(ISender sender, int userId, int accessId)
    {
        await sender.Send(new RemoveUserAccessCommand
        {
            UserId = userId,
            AccessId = accessId
        });

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get User Permissions")]
    [EndpointDescription("Retrieves user permissions along with their items.")]
    public static async Task<Ok<List<UserPermissionDto>>> GetUserPermissions(ISender sender, int userId)
    {
        var vm = await sender.Send(new GetUserPermissionsQuery(UserId: userId));

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get User Access Permissions")]
    [EndpointDescription("Retrieves user access permissions along with their items.")]
    public static async Task<Ok<List<UserAccessPermissionDto>>> GetUserAccessPermissions(ISender sender, [AsParameters] GetAccessPermissionsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Create user permission")]
    [EndpointDescription("Creates a permission to user using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateUserPermission(ISender sender, int userId, int permissionId)
    {
        var result = await sender.Send(new CreateUserPermissionCommand
        {
            UserId = userId,
            PermissionId = permissionId
        });

        return TypedResults.Created($"/api/Users/{userId}/permission/{result}", result);
    }

    [EndpointSummary("Delete a permission from a user")]
    [EndpointDescription("Deletes a permission from a user with the specified IDs.")]
    public static async Task<NoContent> DeleteUserPermission(ISender sender, int userId, int id)
    {
        await sender.Send(new DeleteUserPermissionCommand
        {
            Id = id,
            UserId = userId
        });

        return TypedResults.NoContent();
    }
}
