using Delex_POS.Application.CancelReasons.Commands.CreateCancelReason;
using Delex_POS.Application.CancelReasons.Commands.DeleteCancelReason;
using Delex_POS.Application.CancelReasons.Commands.UpdateCancelReason;
using Delex_POS.Application.CancelReasons.Queries.CancelReasonDTOs;
using Delex_POS.Application.CancelReasons.Queries.GetCancelReason;
using Delex_POS.Application.CancelReasons.Queries.GetCancelReasons;
using Delex_POS.Application.CancelReasons.Queries.GetCancelReasonsList;
using Delex_POS.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class CancelReason : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateCancelReason);
        groupBuilder.MapPut(UpdateCancelReason, "{id}");
        groupBuilder.MapDelete(DeleteCancelReason, "{id}");

        groupBuilder.MapGet(GetCancelReasons);
        groupBuilder.MapGet(GetCancelReasonsList, "list");
        groupBuilder.MapGet(GetCancelReason, "{id}");
    }

    [EndpointSummary("Create a new Cancel Reason")]
    [EndpointDescription("Creates a new cancel reason using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateCancelReason(ISender sender, CreateCancelReasonCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Update an Cancel Reason")]
    [EndpointDescription("Updates the specified cancel reason. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateCancelReason(ISender sender, int id, UpdateCancelReasonCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Cancel Reason")]
    [EndpointDescription("Deletes the cancel reason with the specified ID.")]
    public static async Task<NoContent> DeleteCancelReason(ISender sender, int id)
    {
        await sender.Send(new DeleteCancelReasonCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all Cancel Reasons")]
    [EndpointDescription("Retrieves all cancel reasons.")]
    public static async Task<Ok<PaginatedList<CancelReasonDto>>> GetCancelReasons(ISender sender, [AsParameters] GetCancelReasonsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Cancel Reasons List")]
    [EndpointDescription("Retrieves all cancel reasons as a list.")]
    public static async Task<Ok<List<CancelReasonListDto>>> GetCancelReasonsList(ISender sender, [AsParameters] GetCancelReasonsListQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Cancel Reason")]
    [EndpointDescription("Retrieves a cancel reason.")]
    public static async Task<Ok<CancelReasonDto>> GetCancelReason(ISender sender, string id, [AsParameters] GetCancelReasonQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
