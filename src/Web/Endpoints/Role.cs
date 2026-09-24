using Delex_POS.Application.Roles.Commands.CreateRole;
using Delex_POS.Application.Roles.Commands.DeleteRole;
using Delex_POS.Application.Roles.Commands.AddRoleAccess;
using Delex_POS.Application.Roles.Commands.RemoveRoleAccess;
using Delex_POS.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Delex_POS.Application.Roles.Queries.RoleDTOs;
using Delex_POS.Application.Roles.Queries.GetRoles;
using Delex_POS.Application.Roles.Queries.GetRole;
using Delex_POS.Application.Roles.Queries.GetRoleAccess;

namespace Delex_POS.Web.Endpoints;

public class Role : IEndpointGroup
{
    public static string? RoutePrefix => "/api/Roles";

    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateRole);
        groupBuilder.MapDelete(DeleteRole, "{roleId}");
        groupBuilder.MapGet(GetRoles);
        groupBuilder.MapGet(GetRole, "{roleId}");

        groupBuilder.MapGet(GetRoleAccess, "{roleId}/accesses");
        groupBuilder.MapPost(AddRoleAccess, "{roleId}/accesses");
        groupBuilder.MapDelete(RemoveRoleAccess, "{roleId}/accesses/{accessId}");
    }

    [EndpointSummary("Create a new Role")]
    [EndpointDescription("Creates a new role using the provided details and returns the ID of the created item.")]
    public static async Task<Created<string>> CreateRole(ISender sender, CreateRoleCommand command)
    {
        (string id, string name) result = await sender.Send(command);

        return TypedResults.Created($"/api/Roles/{result.id}", result.id);
    }

    [EndpointSummary("Delete a Role")]
    [EndpointDescription("Deletes the role with the specified ID.")]
    public static async Task<NoContent> DeleteRole(ISender sender, string roleId)
    {
        await sender.Send(new DeleteRoleCommand(roleId));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all roles")]
    [EndpointDescription("Retrieves all roles.")]
    public static async Task<Ok<PaginatedList<IdentityRoleDto>>> GetRoles(ISender sender,[AsParameters] GetRolesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
    
    [EndpointSummary("Get role")]
    [EndpointDescription("Retrieves a role.")]
    public static async Task<Ok<IdentityRoleDto>> GetRole(ISender sender, string roleId)
    {
        var vm = await sender.Send(new GetRoleQuery { RoleId = roleId });

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get all role access")]
    [EndpointDescription("Retrieves all role access.")]
    public static async Task<Ok<PaginatedList<RoleAccessDto>>> GetRoleAccess(ISender sender, [AsParameters] GetRoleAccessQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Add a aole access")]
    [EndpointDescription("Adds a role access using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> AddRoleAccess(ISender sender, string roleId, AddRoleAccessCommand command)
    {
        command.RoleId = roleId;
        var result = await sender.Send(command);

        return TypedResults.Created($"/api/Roles/{roleId}/accesses/{result}", result);
    }

    [EndpointSummary("Remove a Role Access")]
    [EndpointDescription("Remove a role access with the specified IDs.")]
    public static async Task<NoContent> RemoveRoleAccess(ISender sender, string roleId, int accessId)
    {
        await sender.Send(new RemoveRoleAccessCommand
        {
            RoleId = roleId,
            AccessId = accessId
        });

        return TypedResults.NoContent();
    }
}