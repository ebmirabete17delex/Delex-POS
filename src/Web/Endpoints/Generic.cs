using Delex_POS.Application.GenericItems.Queries.GetGeneric;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.GenericItems.Commands.CreateGeneric;
using Delex_POS.Application.GenericItems.Commands.DeleteGeneric;
using Delex_POS.Application.GenericItems.Commands.UpdateGeneric;
using Delex_POS.Application.GenericItems.Queries.GenericDTOs;
using Delex_POS.Application.GenericItems.Queries.GetGenerics;
using Delex_POS.Application.GenericItems.Queries.GetGenericsList;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class Generic : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateGeneric);
        groupBuilder.MapPut(UpdateGeneric, "{id}");
        groupBuilder.MapDelete(DeleteGeneric, "{id}");

        groupBuilder.MapGet(GetGenericItems);
        groupBuilder.MapGet(GetGenericList, "list");
        groupBuilder.MapGet(GetGeneric, "{id}");
    }

    [EndpointSummary("Create a new Generic")]
    [EndpointDescription("Creates a new generic item using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateGeneric(ISender sender, CreateGenericCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Update a Generic")]
    [EndpointDescription("Updates the specified generic item. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateGeneric(ISender sender, int id, UpdateGenericCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Generic")]
    [EndpointDescription("Deletes the generic item with the specified ID.")]
    public static async Task<NoContent> DeleteGeneric(ISender sender, int id)
    {
        await sender.Send(new DeleteGenericCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get generic Items")]
    [EndpointDescription("Retrieves all generic items.")]
    public static async Task<Ok<PaginatedList<GenericDto>>> GetGenericItems(ISender sender, [AsParameters] GetGenericsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get generic items List")]
    [EndpointDescription("Retrieves all generic items as a list.")]
    public static async Task<Ok<List<GenericsListDto>>> GetGenericList(ISender sender, [AsParameters] GetGenericsListQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get generic item")]
    [EndpointDescription("Retrieves a generic item by its ID.")]
    public static async Task<Ok<GenericDto>> GetGeneric(ISender sender, int id, [AsParameters] GetGenericQuery query)
    {
        var vm = await sender.Send(new GetGenericQuery(id));

        return TypedResults.Ok(vm);
    }
}
