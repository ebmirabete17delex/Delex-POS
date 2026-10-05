using Delex_POS.Application.Common.Models;
using Delex_POS.Application.ProductCategories.Commands.CreateProductCategory;
using Delex_POS.Application.ProductCategories.Commands.DeleteProductCategory;
using Delex_POS.Application.ProductCategories.Queries.GetProductCategories;
using Delex_POS.Application.ProductCategories.Queries.ProductCategoryDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class ProductCategory : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();
        groupBuilder.MapPost(CreateProductCategory);
        groupBuilder.MapDelete(DeleteProductCategory, "{id}");
        groupBuilder.MapGet(GetProductCategories);
    }

    [EndpointSummary("Create a Product Category")]
    [EndpointDescription("Creates a new product category using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateProductCategory(ISender sender, CreateProductCategoryCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Delete a Product Category")]
    [EndpointDescription("Deletes the product category with the specified ID.")]
    public static async Task<NoContent> DeleteProductCategory(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCategoryCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all product categories")]
    [EndpointDescription("Retrieves all product categories.")]
    public static async Task<Ok<List<ProductCategoryDto>>> GetProductCategories(ISender sender, [AsParameters] GetProductCategoriesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
