using Delex_POS.Application.AccessClaims.Commands.CreateAccessClaim;
using Delex_POS.Application.AccessClaims.Commands.UpdateAccessClaim;
using Delex_POS.Application.AccessClaims.Commands.DeleteAccessClaim;
using Delex_POS.Application.AccessClaims.Commands.CreatePermission;
using Delex_POS.Application.AccessClaims.Commands.DeletePermission;
using Delex_POS.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Delex_POS.Application.AccessClaims.Queries.GetAccessClaimsList;
using Delex_POS.Application.AccessClaims.Queries.GetAccessClaims;
using Delex_POS.Application.AccessClaims.Queries.GetAccessClaim;
using Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;
using Delex_POS.Application.AccessClaims.Queries.GetAccessPermissions;

namespace Delex_POS.Web.Endpoints;

public class Access : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateAccessClaim);
        groupBuilder.MapPut(UpdateAccessClaim, "{id}");
        groupBuilder.MapDelete(DeleteAccessClaim, "{id}");
        groupBuilder.MapGet(GetAccessClaims);
        groupBuilder.MapGet(GetAccessClaimsList, "list");
        groupBuilder.MapGet(GetAccessClaim, "{id}");

        groupBuilder.MapPost(CreateAccessPermission, "{accessId}/permission");
        groupBuilder.MapDelete(DeleteAccessPermission, "{accessId}/permission/{id}");
        groupBuilder.MapGet(GetAccessPermissions, "{accessId}/permission");
    }


    [EndpointSummary("Create a new Access")]
    [EndpointDescription("Creates a new access using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateAccessClaim(ISender sender, CreateAccessClaimCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/access/{id}", id);
    }

    [EndpointSummary("Update an Access")]
    [EndpointDescription("Updates the specified Access. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateAccessClaim(ISender sender, int id, UpdateAccessClaimCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a access")]
    [EndpointDescription("Deletes the access with the specified ID.")]
    public static async Task<NoContent> DeleteAccessClaim(ISender sender, int id)
    {
        await sender.Send(new DeleteAccessClaimCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all accesses")]
    [EndpointDescription("Retrieves all accesses.")]
    public static async Task<Ok<PaginatedList<AccessClaimDto>>> GetAccessClaims(ISender sender, [AsParameters] GetAccessClaimsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get accesses List")]
    [EndpointDescription("Retrieves all accesses as list.")]
    public static async Task<Ok<List<AccessClaimListDto>>> GetAccessClaimsList(ISender sender, [AsParameters] GetAccessClaimsListQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get access")]
    [EndpointDescription("Retrieves a access.")]
    public static async Task<Ok<AccessClaimDto>> GetAccessClaim(ISender sender, int id)
    {
        var vm = await sender.Send(new GetAccessClaimQuery { Id = id });

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Create a new Access Permission")]
    [EndpointDescription("Creates a new access permission using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateAccessPermission(ISender sender, int accessId, CreatePermissionCommand command)
    {
        command.AccessId = accessId;
        int id = await sender.Send(command);

        return TypedResults.Created($"/access/{accessId}/permission/{id}", id);
    }

    [EndpointSummary("Delete a access permission")]
    [EndpointDescription("Deletes the access with the specified ID.")]
    public static async Task<NoContent> DeleteAccessPermission(ISender sender, [AsParameters] DeletePermissionCommand command)
    {
        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get accesses permissions")]
    [EndpointDescription("Retrieves all accesses persmissions.")]
    public static async Task<Ok<List<AccessPermissionDto>>> GetAccessPermissions(ISender sender, [AsParameters] GetAccessPermissionsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

}
