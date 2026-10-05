using Delex_POS.Application.Common.Models;
using Delex_POS.Application.ProductClasses.Commands.CreateProductClass;
using Delex_POS.Application.ProductClasses.Commands.DeleteProductClass;
using Delex_POS.Application.ProductClasses.Commands.UpdateProductClass;
using Delex_POS.Application.ProductClasses.Queries.GetProductClasses;
using Delex_POS.Application.ProductClasses.Queries.GetProductClass;
using Delex_POS.Application.ProductClasses.Queries.GetProductClassesList;
using Delex_POS.Application.ProductClasses.Queries.ProductClassDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class ProductClass : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateProductClass);
        groupBuilder.MapPut(UpdateProductClass, "{id}");
        groupBuilder.MapDelete(DeleteProductClass, "{id}");
        
        groupBuilder.MapGet(GetProductClasses);
        groupBuilder.MapGet(GetProductClassesList, "list");
        groupBuilder.MapGet(GetProductClass, "{id}");
    }

    [EndpointSummary("Create a new product class")]
    [EndpointDescription("Creates a new product class using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateProductClass(ISender sender, CreateProductClassCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Update a product class")]
    [EndpointDescription("Updates the specified product class. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateProductClass(ISender sender, int id, UpdateProductClassCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a product class")]
    [EndpointDescription("Deletes the product class with the specified ID.")]
    public static async Task<NoContent> DeleteProductClass(ISender sender, int id)
    {
        await sender.Send(new DeleteProductClassCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all product classes")]
    [EndpointDescription("Retrieves all product classes.")]
    public static async Task<Ok<PaginatedList<ProductClassDto>>> GetProductClasses(ISender sender, [AsParameters] GetProductClassesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get product classes List")]
    [EndpointDescription("Retrieves all product classes as list.")]
    public static async Task<Ok<List<ProductClassListDto>>> GetProductClassesList(ISender sender, [AsParameters] GetProductClassesListQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Product class")]
    [EndpointDescription("Retrieves a product class.")]
    public static async Task<Ok<ProductClassDto>> GetProductClass(ISender sender, string id, [AsParameters] GetProductClassQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
