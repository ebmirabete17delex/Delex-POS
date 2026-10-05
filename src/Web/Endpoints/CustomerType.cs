using Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;
using Delex_POS.Application.AccessClaims.Queries.GetAccessClaim;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.CustomerTypes.Commands.CreateCustomerType;
using Delex_POS.Application.CustomerTypes.Commands.DeleteCustomerType;
using Delex_POS.Application.CustomerTypes.Commands.UpdateCustomerType;
using Delex_POS.Application.CustomerTypes.Queries.CustomerTypeDTOs;
using Delex_POS.Application.CustomerTypes.Queries.GetCustomerType;
using Delex_POS.Application.CustomerTypes.Queries.GetCustomerTypes;
using Delex_POS.Application.CustomerTypes.Queries.GetCustomerTypesList;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class CustomerType : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateCustomerType);
        groupBuilder.MapPut(UpdateCustomerType, "{id}");
        groupBuilder.MapDelete(DeleteCustomerType, "{id}");
        
        groupBuilder.MapGet(GetCustomerTypes);
        groupBuilder.MapGet(GetCustomerTypesList, "list");
        groupBuilder.MapGet(GetCustomerType, "{id}");
    }

    [EndpointSummary("Create a new Customer Type")]
    [EndpointDescription("Creates a new customer type using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateCustomerType(ISender sender, CreateCustomerTypeCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Update an Customer Type")]
    [EndpointDescription("Updates the specified customer type. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateCustomerType(ISender sender, int id, UpdateCustomerTypeCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Customer Type")]
    [EndpointDescription("Deletes the customer type with the specified ID.")]
    public static async Task<NoContent> DeleteCustomerType(ISender sender, int id)
    {
        await sender.Send(new DeleteCustomerTypeCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all Customer Types")]
    [EndpointDescription("Retrieves all customer types.")]
    public static async Task<Ok<PaginatedList<CustomerTypeDto>>> GetCustomerTypes(ISender sender, [AsParameters] GetCustomerTypesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Customer Types List")]
    [EndpointDescription("Retrieves all customer types as a list.")]
    public static async Task<Ok<List<CustomerTypeDto>>> GetCustomerTypesList(ISender sender, [AsParameters] GetCustomerTypesListQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
    [EndpointSummary("Get Customer Type")]
    [EndpointDescription("Retrieves a customer type by its ID.")]
    public static async Task<Ok<CustomerTypeDto>> GetCustomerType(ISender sender, int id, [AsParameters] GetCustomerTypeQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
