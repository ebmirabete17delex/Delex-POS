using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Customers.Queries.CustomerDTOs;
using Delex_POS.Application.Customers.Queries.GetCustomer;
using Delex_POS.Application.Customers.Queries.GetCustomers;
using Delex_POS.Application.Customers.Queries.SearchCustomer;
using Delex_POS.Application.Customers.Commands.CreateCustomer;
using Delex_POS.Application.Customers.Commands.DeleteCustomer;
using Delex_POS.Application.Customers.Commands.UpdateCustomer;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class Customer : IEndpointGroup
{
    public static string? RoutePrefix => "/api/customers";
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(CreateCustomer)
            .RequireAuthorization("CustomerCreate");
        groupBuilder.MapPut(UpdateCustomer, "{id}")
            .RequireAuthorization("CustomerUpdate");
        groupBuilder.MapDelete(DeleteCustomer, "{id}")
            .RequireAuthorization("CustomerDelete");

        groupBuilder.MapGet(GetCustomers)
            .RequireAuthorization("CustomerRead");
        groupBuilder.MapGet(GetCustomer, "{id}")
            .RequireAuthorization("CustomerRead");
        groupBuilder.MapGet(SearchCustomer, "/search/{query}")
            .RequireAuthorization("CustomerRead");
    }

    [EndpointSummary("Get all Customers")]
    [EndpointDescription("Retrieves all customers along with their items.")]
    public static async Task<Ok<PaginatedList<CustomerListDto>>> GetCustomers(ISender sender, [AsParameters] GetCustomersQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Customer")]
    [EndpointDescription("Retrieves customer along with their items.")]
    public static async Task<Ok<CustomerDto>> GetCustomer(ISender sender, int id)
    {
        var vm = await sender.Send(new GetCustomerQuery(Id: id));

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Customer")]
    [EndpointDescription("Retrieves customer along with their items.")]
    public static async Task<Ok<List<CustomerSearchListDto>>> SearchCustomer(ISender sender, string query)
    {
        var vm = await sender.Send(new SearchCustomerQuery(Query: query));

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Create a new Customer")]
    [EndpointDescription("Creates a new customer using the provided details and returns the ID of the created list.")]
    public static async Task<Created<int>> CreateCustomer(ISender sender, CreateCustomerCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/customers/{id}", id);
    }

    [EndpointSummary("Update a Customer")]
    [EndpointDescription("Updates the specified Customer. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateCustomer(ISender sender, int id, UpdateCustomerCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a customer")]
    [EndpointDescription("Deletes the customer with the specified ID.")]
    public static async Task<NoContent> DeleteCustomer(ISender sender, int id)
    {
        await sender.Send(new DeleteCustomerCommand(id));

        return TypedResults.NoContent();
    }
}
