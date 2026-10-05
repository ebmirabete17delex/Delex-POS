using Delex_POS.Application.Common.Models;
using Delex_POS.Application.ProductAttributes.Commands.CreateProductAttribute;
using Delex_POS.Application.ProductAttributes.Commands.DeleteProductAttribute;
using Delex_POS.Application.ProductAttributes.Commands.UpdateProductAttribute;
using Delex_POS.Application.ProductAttributes.Queries.GetProductAttributes;
using Delex_POS.Application.ProductAttributes.Queries.GetProductAttribute;
using Delex_POS.Application.ProductAttributes.Queries.ProductAttributeDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class ProductAttribute : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();
        groupBuilder.MapPost(CreateProductAttribute);
        groupBuilder.MapPut(UpdateProductAttribute, "{id}");
        groupBuilder.MapDelete(DeleteProductAttribute, "{id}");
        groupBuilder.MapGet(GetProductAttributes);
        groupBuilder.MapGet(GetProductAttribute, "{id}");
    }

    [EndpointSummary("Create a new Product Attribute")]
    [EndpointDescription("Creates a new product attribute using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateProductAttribute(ISender sender, CreateProductAttributeCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Update a Product Attribute")]
    [EndpointDescription("Updates the specified product attribute. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateProductAttribute(ISender sender, int id, UpdateProductAttributeCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Product Attribute")]
    [EndpointDescription("Deletes the product attribute with the specified ID.")]
    public static async Task<NoContent> DeleteProductAttribute(ISender sender, int id)
    {
        await sender.Send(new DeleteProductAttributeCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all Product Attributes")]
    [EndpointDescription("Retrieves all product attributes.")]
    public static async Task<Ok<PaginatedList<ProductAttributeDto>>> GetProductAttributes(ISender sender, [AsParameters] GetProductAttributesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Product Attribute")]
    [EndpointDescription("Retrieves a specific product attribute.")]
    public static async Task<Ok<ProductAttributeDto>> GetProductAttribute(ISender sender, int id)
    {
        var vm = await sender.Send(new GetProductAttributeQuery(id));

        return TypedResults.Ok(vm);
    }
}
