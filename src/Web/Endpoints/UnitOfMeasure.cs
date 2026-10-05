using Delex_POS.Application.Common.Models;
using Delex_POS.Application.UnitOfMeasures.Commands.CreateUnitOfMeasure;
using Delex_POS.Application.UnitOfMeasures.Commands.DeleteUnitOfMeasure;
using Delex_POS.Application.UnitOfMeasures.Commands.UpdateUnitOfMeasure;
using Delex_POS.Application.UnitOfMeasures.Queries.GetUnitOfMeasure;
using Delex_POS.Application.UnitOfMeasures.Queries.GetUnitsOfMeasure;
using Delex_POS.Application.UnitOfMeasures.Queries.GetUnitsOfMeasureList;
using Delex_POS.Application.UnitOfMeasures.Queries.UnitOfMeasureDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class UnitOfMeasure : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateUnitOfMeasure);
        groupBuilder.MapPut(UpdateUnitOfMeasure, "{id}");
        groupBuilder.MapDelete(DeleteUnitOfMeasure, "{id}");
        groupBuilder.MapGet(GetUnitOfMeasures);
        groupBuilder.MapGet(GetUnitOfMeasuresList, "list");
        groupBuilder.MapGet(GetUnitOfMeasure, "{id}");
    }

    [EndpointSummary("Create a new Unit of Measure")]
    [EndpointDescription("Creates a new unit of measure using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateUnitOfMeasure(ISender sender, CreateUnitOfMeasureCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/UnitOfMeasure/{id}", id);
    }

    [EndpointSummary("Update a Unit of Measure")]
    [EndpointDescription("Updates the specified Unit of Measure. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateUnitOfMeasure(ISender sender, int id, UpdateUnitOfMeasureCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Unit of Measure")]
    [EndpointDescription("Deletes the unit of measure with the specified ID.")]
    public static async Task<NoContent> DeleteUnitOfMeasure(ISender sender, int id)
    {
        await sender.Send(new DeleteUnitOfMeasureCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all Unit of Measures")]
    [EndpointDescription("Retrieves all units of measure.")]
    public static async Task<Ok<PaginatedList<UnitOfMeasureDto>>> GetUnitOfMeasures(ISender sender, [AsParameters] GetUnitsOfMeasureQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get unit of measure List")]
    [EndpointDescription("Retrieves all units of measure as a list.")]
    public static async Task<Ok<List<UnitOfMeasureListDto>>> GetUnitOfMeasuresList(ISender sender, [AsParameters] GetUnitsOfMeasureListQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get unit of measure")]
    [EndpointDescription("Retrieves a unit of measure.")]
    public static async Task<Ok<UnitOfMeasureDto>> GetUnitOfMeasure(ISender sender, int id, [AsParameters] GetUnitOfMeasureQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
