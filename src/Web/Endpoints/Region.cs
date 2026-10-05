using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Regions.Commands.CreateRegion;
using Delex_POS.Application.Regions.Commands.DeleteRegion;
using Delex_POS.Application.Regions.Commands.UpdateRegion;
using Delex_POS.Application.Regions.Queries.GetRegion;
using Delex_POS.Application.Regions.Queries.GetRegions;
using Delex_POS.Application.Regions.Queries.GetRegionsList;
using Delex_POS.Application.Regions.Queries.RegionDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class Region : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateRegion);
        groupBuilder.MapPut(UpdateRegion, "{id}");
        groupBuilder.MapDelete(DeleteRegion, "{id}");

        groupBuilder.MapGet(GetRegions);
        groupBuilder.MapGet(GetRegionsList, "list");
        groupBuilder.MapGet(GetRegion, "{id}");
    }

    [EndpointSummary("Create a new Region")]
    [EndpointDescription("Creates a new region using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateRegion(ISender sender, CreateRegionCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Region/{id}", id);
    }

    [EndpointSummary("Update an Region")]
    [EndpointDescription("Updates the specified Region. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateRegion(ISender sender, int id, UpdateRegionCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Region")]
    [EndpointDescription("Deletes the region with the specified ID.")]
    public static async Task<NoContent> DeleteRegion(ISender sender, int id)
    {
        await sender.Send(new DeleteRegionCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all regions")]
    [EndpointDescription("Retrieves all regions.")]
    public static async Task<Ok<PaginatedList<RegionDto>>> GetRegions(ISender sender, [AsParameters] GetRegionsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get regions List")]
    [EndpointDescription("Retrieves all regions as list.")]
    public static async Task<Ok<List<RegionListDto>>> GetRegionsList(ISender sender, [AsParameters] GetRegionsListQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get region")]
    [EndpointDescription("Retrieves a region.")]
    public static async Task<Ok<RegionDto>> GetRegion(ISender sender, int id, [AsParameters] GetRegionQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
