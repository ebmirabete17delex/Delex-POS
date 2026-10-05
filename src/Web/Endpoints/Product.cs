using Delex_POS.Application.Common.Models;
using Delex_POS.Application.ProductPurchasings.Queries.GetProductPurchasing;
using Delex_POS.Application.ProductPurchasings.Commands.CreateProductPurchasing;
using Delex_POS.Application.Products.Commands.CreateProduct;
using Delex_POS.Application.Products.Commands.DeleteProduct;
using Delex_POS.Application.Products.Commands.UpdateProduct;
using Delex_POS.Application.Products.Queries.GetProduct;
using Delex_POS.Application.Products.Queries.GetProducts;
using Delex_POS.Application.Products.Queries.ProductDTOs;
using Delex_POS.Application.ProductSales.Commands.CreateProductSale;
using Delex_POS.Application.ProductSales.Queries.GetProductSale;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class Product : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateProduct);
        groupBuilder.MapPost(CreateProductSale,"{id}/sale");
        groupBuilder.MapPost(CreateProductPurchasing,"{id}/purchasing");
        groupBuilder.MapPut(UpdateProduct, "{id}");
        groupBuilder.MapDelete(DeleteProduct, "{id}");

        groupBuilder.MapGet(GetProducts);
        groupBuilder.MapGet(GetProduct, "{id}");
        groupBuilder.MapGet(GetProductSale, "{id}/sale");
        groupBuilder.MapGet(GetProductPurchasing, "{id}/purchasing");
    }

    [EndpointSummary("Create a new Product")]
    [EndpointDescription("Creates a new product using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateProduct(ISender sender, CreateProductCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/product/{id}", id);
    }

    [EndpointSummary("Create a new Product Sale")]
    [EndpointDescription("Creates a new product sale using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateProductSale(ISender sender, CreateProductSaleCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/product/{id}", id);
    }

    [EndpointSummary("Create a new Product Purchasing")]
    [EndpointDescription("Creates a new product purchasing using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateProductPurchasing(ISender sender, CreateProductPurchasingCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/product/{id}", id);
    }

    [EndpointSummary("Update an Product")]
    [EndpointDescription("Updates the specified Product. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a product")]
    [EndpointDescription("Deletes the product with the specified ID.")]
    public static async Task<NoContent> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all Products")]
    [EndpointDescription("Retrieves all products.")]
    public static async Task<Ok<PaginatedList<ProductDto>>> GetProducts(ISender sender, [AsParameters] GetProductsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get a Product")]
    [EndpointDescription("Retrieves the product with the specified ID.")]
    public static async Task<Ok<ProductDto>> GetProduct(ISender sender, int id)
    {
        var vm = await sender.Send(new GetProductQuery(id));

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Product Sale")]
    [EndpointDescription("Retrieves the product sale information with the specified ID.")]
    public static async Task<Ok<ProductSaleDto>> GetProductSale(ISender sender, int id)
    {
        var vm = await sender.Send(new GetProductSaleQuery(id));

        return TypedResults.Ok(vm);
    }
     
    [EndpointSummary("Get Product Purchasing")]
    [EndpointDescription("Retrieves the product purchasing information with the specified ID.")]
    public static async Task<Ok<ProductPurchasingDto>> GetProductPurchasing(ISender sender, int id)
    {
        var vm = await sender.Send(new GetProductPurchasingQuery(id));

        return TypedResults.Ok(vm);
    }
}
