using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Warehouses.Commands.CreateWarehouse;
using Delex_POS.Application.Warehouses.Commands.DeleteWarehouse;
using Delex_POS.Application.Warehouses.Commands.UpdateWarehouse;
using Delex_POS.Application.Warehouses.Queries.GetWarehouse;
using Delex_POS.Application.Warehouses.Queries.GetWarehouses;
using Delex_POS.Application.Warehouses.Queries.GetWarehousesList;
using Delex_POS.Application.Warehouses.Queries.WarehouseDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class Warehouse : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateWarehouse);
        groupBuilder.MapPut(UpdateWarehouse, "{id}");
        groupBuilder.MapDelete(DeleteWarehouse, "{id}");
        groupBuilder.MapGet(GetWarehouses);
        //groupBuilder.MapGet(GetWarehouseList);
        groupBuilder.MapGet(GetWarehouse, "{id}");

    }
    [EndpointSummary("Create a new Warehouse")]
    [EndpointDescription("Creates a new warehouse using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateWarehouse(ISender sender, CreateWarehouseCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Update an Warehouse")]
    [EndpointDescription("Updates the specified warehouse. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateWarehouse(ISender sender, int id, UpdateWarehouseCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Warehouse")]
    [EndpointDescription("Deletes the warehouse with the specified ID.")]
    public static async Task<NoContent> DeleteWarehouse(ISender sender, int id)
    {
        await sender.Send(new DeleteWarehouseCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all Warehouses")]
    [EndpointDescription("Retrieves all warehouses.")]
    public static async Task<Ok<PaginatedList<WarehouseDto>>> GetWarehouses(ISender sender, [AsParameters] GetWarehousesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    //[EndpointSummary("Get all Warehouses for dropdown list")]
    //[EndpointDescription("Retrieves all warehouses for dropdown list.")]
    //public static async Task<Ok<List<WarehouseListDto>>> GetWarehouseList(ISender sender, [AsParameters] GetWarehousesListQuery query)
    //{
    //    var vm = await sender.Send(query);

    //    return TypedResults.Ok(vm);
    //}

    [EndpointSummary("Get warehouse")]
    [EndpointDescription("Retrieves a warehouse.")]
    public static async Task<Ok<WarehouseDto>> GetWarehouse(ISender sender, [AsParameters] GetWarehouseQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
