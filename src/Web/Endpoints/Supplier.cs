using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Suppliers.Commands.CreateSupplier;
using Delex_POS.Application.Suppliers.Commands.DeleteSupplier;
using Delex_POS.Application.Suppliers.Commands.UpdateSupplier;
using Delex_POS.Application.Suppliers.Queries.SupplierDTOs;
using Delex_POS.Application.Suppliers.Queries.GetSupplier;
using Delex_POS.Application.Suppliers.Queries.GetSuppliers;
using Delex_POS.Application.Suppliers.Queries.SearchSuppliers;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Delex_POS.Web.Endpoints;

public class Supplier : IEndpointGroup
{
    public static string? RoutePrefix => "/api/suppliers";
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(CreateSupplier);
        groupBuilder.MapPut(UpdateSupplier, "{id}");
        groupBuilder.MapDelete(DeleteSupplier, "{id}");

        groupBuilder.MapGet(GetSuppliers);
        groupBuilder.MapGet(GetSupplier, "{id}");
        groupBuilder.MapGet(SearchSupplier, "/search/{query}");
    }
    [EndpointSummary("Get all Suppliers")]
    [EndpointDescription("Retrieves all Suppliers along with their items.")]
    public static async Task<Ok<PaginatedList<SupplierListDto>>> GetSuppliers(ISender sender, [AsParameters] GetSuppliersQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Supplier")]
    [EndpointDescription("Retrieves Supplier along with their items.")]
    public static async Task<Ok<SupplierDto>> GetSupplier(ISender sender, int id)
    {
        var vm = await sender.Send(new GetSupplierQuery(Id: id));

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Supplier")]
    [EndpointDescription("Retrieves Supplier along with their items.")]
    public static async Task<Ok<List<SupplierSearchListDto>>> SearchSupplier(ISender sender, string query)
    {
        var vm = await sender.Send(new SearchSuppliersQuery(Query: query));

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Create a new Supplier")]
    [EndpointDescription("Creates a new Supplier using the provided details and returns the ID of the created list.")]
    public static async Task<Created<int>> CreateSupplier(ISender sender, CreateSupplierCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/Suppliers/{id}", id);
    }

    [EndpointSummary("Update a Supplier")]
    [EndpointDescription("Updates the specified Supplier. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateSupplier(ISender sender, int id, UpdateSupplierCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Supplier")]
    [EndpointDescription("Deletes the Supplier with the specified ID.")]
    public static async Task<NoContent> DeleteSupplier(ISender sender, int id)
    {
        await sender.Send(new DeleteSupplierCommand(id));

        return TypedResults.NoContent();
    }
}
