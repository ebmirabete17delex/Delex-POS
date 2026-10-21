//using Delex_POS.Application.Common.Models;
//using Delex_POS.Application.Products.Commands.CreateProduct;
//using Delex_POS.Application.Products.Queries.GetProducts;
//using Microsoft.AspNetCore.Http.HttpResults;
//using System.Security.Claims;

//namespace Delex_POS.Web.Endpoints.Examples;

///// <summary>
///// Example endpoint group demonstrating RBAC (Role-Based Access Control) with JWT.
///// Copy this pattern to implement authorization on your existing endpoints.
///// 
///// This example shows:
///// - How to protect endpoints with roles
///// - How to access user claims in endpoint handlers
///// - How to check roles programmatically
///// - How to combine multiple authorization requirements
///// </summary>
//public class ProductExample : IEndpointGroup
//{
//    public static string? RoutePrefix => "/api/products";

//    public static void Map(RouteGroupBuilder groupBuilder)
//    {
//        // Public endpoint - Any authenticated user can read
//        groupBuilder.MapGet(GetAllProducts, "")
//            .RequireAuthorization("AnyAuthenticated")
//            .WithName("GetAllProducts")
//            .WithOpenApi();

//        // Manager+ only - Create new products
//        groupBuilder.MapPost(CreateProduct, "")
//            .RequireAuthorization("AdministratorOrManager")
//            .WithName("CreateProduct")
//            .WithOpenApi();

//        // Manager+ only - Update products
//        groupBuilder.MapPut(UpdateProduct, "{id}")
//            .RequireAuthorization("AdministratorOrManager")
//            .WithName("UpdateProduct")
//            .WithOpenApi();

//        // Admin only - Delete products
//        groupBuilder.MapDelete(DeleteProduct, "{id}")
//            .RequireAuthorization("Administrator")
//            .WithName("DeleteProduct")
//            .WithOpenApi();

//        // Examples with custom authorization checks
//        groupBuilder.MapGet(GetProductWithAudit, "{id}/audit")
//            .RequireAuthorization("AdministratorOrManager")
//            .WithName("GetProductWithAudit")
//            .WithOpenApi();

//        groupBuilder.MapGet(GetMyPermissions, "my-permissions")
//            .RequireAuthorization()
//            .WithName("GetMyPermissions")
//            .WithOpenApi();
//    }

//    [EndpointSummary("Get all products")]
//    [EndpointDescription("Retrieves all products (accessible to authenticated users).")]
//    public static async Task<Ok<List<ProductDto>>> GetAllProducts(
//        ISender sender,
//        ClaimsPrincipal user)
//    {
//        // Get current user info from JWT claims
//        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
//        var userName = user.FindFirst(ClaimTypes.Name)?.Value;

//        // TODO: Log user action
//        // _logger.LogInformation("User {UserId} ({UserEmail}) retrieved all products", userId, userEmail);

//        var result = await sender.Send(new GetProductsQuery());
//        return TypedResults.Ok(result);
//    }

//    [EndpointSummary("Create a new product")]
//    [EndpointDescription("Creates a new product (Manager+ only).")]
//    public static async Task<Created<int>> CreateProduct(
//        ISender sender,
//        CreateProductCommand command,
//        ClaimsPrincipal user)
//    {
//        // Get current user info
//        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        var userRoles = user.FindAll(ClaimTypes.Role);

//        // Verify role (already done by middleware, but shown for clarity)
//        if (!user.IsInRole("Manager") && !user.IsInRole("Administrator"))
//        {
//            return TypedResults.Created("", 0); // Should not reach here due to authorization
//        }

//        // Set audit info
//        command.CreatedBy = userId;

//        var id = await sender.Send(command);

//        // TODO: Log creation
//        // _logger.LogInformation("Product {ProductId} created by user {UserId}", id, userId);

//        return TypedResults.Created($"/api/products/{id}", id);
//    }

//    [EndpointSummary("Update a product")]
//    [EndpointDescription("Updates an existing product (Manager+ only).")]
//    public static async Task<Results<NoContent, NotFound>> UpdateProduct(
//        ISender sender,
//        int id,
//        UpdateProductCommand command,
//        ClaimsPrincipal user)
//    {
//        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//        command.Id = id;
//        command.LastModifiedBy = userId;

//        // TODO: Check if user has permission to modify this specific product
//        // For example, a manager might only be able to modify products in their branch

//        await sender.Send(command);

//        // TODO: Log update
//        // _logger.LogInformation("Product {ProductId} updated by user {UserId}", id, userId);

//        return TypedResults.NoContent();
//    }

//    [EndpointSummary("Delete a product")]
//    [EndpointDescription("Deletes a product (Administrator only).")]
//    public static async Task<Results<NoContent, NotFound>> DeleteProduct(
//        ISender sender,
//        int id,
//        ClaimsPrincipal user)
//    {
//        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//        // Verify admin role (already done by middleware)
//        if (!user.IsInRole("Administrator"))
//        {
//            return TypedResults.NotFound(); // Should not reach here
//        }

//        await sender.Send(new DeleteProductCommand(id));

//        // TODO: Log deletion (audit trail)
//        // _logger.LogWarning("Product {ProductId} deleted by administrator {UserId}", id, userId);

//        return TypedResults.NoContent();
//    }

//    [EndpointSummary("Get product with audit trail")]
//    [EndpointDescription("Retrieves a product with its audit trail (Manager+ only).")]
//    public static async Task<Ok<ProductWithAuditDto>> GetProductWithAudit(
//        ISender sender,
//        int id,
//        ClaimsPrincipal user)
//    {
//        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        var isAdmin = user.IsInRole("Administrator");
//        var isManager = user.IsInRole("Manager");

//        // TODO: Implement logic to check if user can view this product's audit trail
//        // For example, a manager can only see audit for products in their branch

//        var result = await sender.Send(new GetProductWithAuditQuery { ProductId = id });
//        return TypedResults.Ok(result);
//    }

//    [EndpointSummary("Get my permissions")]
//    [EndpointDescription("Returns the current user's roles and permissions.")]
//    public static Task<Ok<UserPermissionsDto>> GetMyPermissions(ClaimsPrincipal user)
//    {
//        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        var email = user.FindFirst(ClaimTypes.Email)?.Value;
//        var name = user.FindFirst(ClaimTypes.Name)?.Value;
//        var userCode = user.FindFirst("user_code")?.Value;

//        // Get all roles
//        var roles = user.FindAll(ClaimTypes.Role)
//            .Select(c => c.Value)
//            .ToList();

//        var permissions = new UserPermissionsDto
//        {
//            UserId = userId,
//            Email = email,
//            Name = name,
//            UserCode = userCode,
//            Roles = roles,
//            IsAdministrator = user.IsInRole("Administrator"),
//            IsManager = user.IsInRole("Manager"),
//            IsStaff = user.IsInRole("Staff"),
//            IsUser = user.IsInRole("User"),
//        };

//        return Task.FromResult(TypedResults.Ok(permissions));
//    }
//}

//// DTOs for this example
//public record ProductDto(int Id, string Name, decimal Price);
//public record UpdateProductCommand(int Id, string Name, decimal Price)
//{
//    public string? CreatedBy { get; set; }
//    public string? LastModifiedBy { get; set; }
//}
//public record DeleteProductCommand(int Id);
//public record GetProductsQuery;
//public record GetProductWithAuditQuery { public int ProductId { get; set; } }
//public record ProductWithAuditDto(int Id, string Name, List<AuditEntryDto> Audit);
//public record AuditEntryDto(DateTime Date, string User, string Action, string Details);

//public record UserPermissionsDto
//{
//    public string? UserId { get; set; }
//    public string? Email { get; set; }
//    public string? Name { get; set; }
//    public string? UserCode { get; set; }
//    public List<string> Roles { get; set; } = new();
//    public bool IsAdministrator { get; set; }
//    public bool IsManager { get; set; }
//    public bool IsStaff { get; set; }
//    public bool IsUser { get; set; }
//}
