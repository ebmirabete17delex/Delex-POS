using Delex_POS.Application.Roles.Commands.CreateRole;
using Delex_POS.Application.Roles.Commands.DeleteRole;
using Delex_POS.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Delex_POS.Application.Roles.Queries.RoleDTOs;
using Delex_POS.Application.Roles.Queries.GetRoles;
using Delex_POS.Application.Roles.Queries.GetRole;
using Microsoft.AspNetCore.Mvc;

namespace Delex_POS.Web.Endpoints;

public class Role : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateRole);
        groupBuilder.MapDelete(DeleteRole, "{id}");
        groupBuilder.MapGet(GetRoles);
        groupBuilder.MapGet(GetRole, "{id}");
    }

    [EndpointSummary("Create a new Role")]
    [EndpointDescription("Creates a new role using the provided details and returns the ID of the created item.")]
    public static async Task<Created<string>> CreateRole(ISender sender, CreateRoleCommand command)
    {
        (string id, string name) result = await sender.Send(command);

        return TypedResults.Created($"/Role/{result.id}", result.name);
    }

    [EndpointSummary("Delete a Role")]
    [EndpointDescription("Deletes the role with the specified ID.")]
    public static async Task<NoContent> DeleteRole(ISender sender, string id)
    {
        await sender.Send(new DeleteRoleCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all roles")]
    [EndpointDescription("Retrieves all roles.")]
    public static async Task<Ok<PaginatedList<IdentityRoleDto>>> GetRoles(ISender sender,[FromBody] GetRolesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
    
    [EndpointSummary("Get role")]
    [EndpointDescription("Retrieves a role.")]
    public static async Task<Ok<IdentityRoleDto>> GetRole(ISender sender, string id, [AsParameters] GetRoleQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
