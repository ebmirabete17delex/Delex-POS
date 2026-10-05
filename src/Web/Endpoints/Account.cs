using Delex_POS.Application.Accounts.Commands.CreateCustomerAccount;
using Delex_POS.Application.Accounts.Commands.CreateEmployeeAccount;
using Delex_POS.Application.Accounts.Commands.CreatePersonalInfo;
using Delex_POS.Application.Accounts.Commands.CreateSupplierAccount;
using Delex_POS.Application.Accounts.Commands.DeleteAccount;
using Delex_POS.Application.Accounts.Commands.UpdateAccount;
using Delex_POS.Application.Accounts.Queries.AccountDTOs;
using Delex_POS.Application.Accounts.Queries.GetAccount;
using Delex_POS.Application.Accounts.Queries.GetCustomerAccounts;
using Delex_POS.Application.Accounts.Queries.GetEmployeeAccounts;
using Delex_POS.Application.Accounts.Queries.GetPersonalInfo;
using Delex_POS.Application.Accounts.Queries.GetSupplierAccounts;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Accounts.Commands.DeletePersonalInfo;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class Account : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateCustomerAccount, "customer");
        groupBuilder.MapPost(CreateEmployeeAccount, "employee");
        groupBuilder.MapPost(CreateSupplierAccount, "supplier");
        groupBuilder.MapPost(CreatePersonalInfo, "{acctId}/personalinfo");
        groupBuilder.MapPut(UpdateAccount, "{id}");
        groupBuilder.MapDelete(DeleteAccount, "{id}");

        groupBuilder.MapGet(GetCustomerAccounts,"customer");
        groupBuilder.MapGet(GetEmployeeAccounts,"employee");
        groupBuilder.MapGet(GetSupplierAccounts,"supplier");
        groupBuilder.MapGet(GetAccount, "{id}");
        groupBuilder.MapGet(GetPersonalInfo, "{id}/personalinfo");

    }

    [EndpointSummary("Create a new Customer Account")]
    [EndpointDescription("Creates a new customer account using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateCustomerAccount(ISender sender, CreateCustomerAccountCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/account/{id}", id);
    }

    [EndpointSummary("Create a new Employee Account")]
    [EndpointDescription("Creates a new employee account using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateEmployeeAccount(ISender sender, CreateEmployeeAccountCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/account/{id}", id);
    }

    [EndpointSummary("Create a new Supplier Account")]
    [EndpointDescription("Creates a new supplier account using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateSupplierAccount(ISender sender, CreateSupplierAccountCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/account/{id}", id);
    }

    [EndpointSummary("Update an Account")]
    [EndpointDescription("Updates the specified Account. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateAccount(ISender sender, int id, UpdateAccountCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete an account")]
    [EndpointDescription("Deletes the account with the specified ID.")]
    public static async Task<NoContent> DeleteAccount(ISender sender, int id)
    {
        await sender.Send(new DeleteAccountCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all customer accounts")]
    [EndpointDescription("Retrieves all customer accounts.")]
    public static async Task<Ok<PaginatedList<AccountDto>>> GetCustomerAccounts(ISender sender, [AsParameters] GetCustomerAccountsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get all employee accounts")]
    [EndpointDescription("Retrieves all employee accounts.")]
    public static async Task<Ok<PaginatedList<AccountDto>>> GetEmployeeAccounts(ISender sender, [AsParameters] GetEmployeeAccountsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get all supplier accounts")]
    [EndpointDescription("Retrieves all supplier accounts.")]
    public static async Task<Ok<PaginatedList<AccountDto>>> GetSupplierAccounts(ISender sender, [AsParameters] GetSupplierAccountsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
    [EndpointSummary("Get Account")]
    [EndpointDescription("Retrieves an account.")]
    public static async Task<Ok<AccountDto>> GetAccount(ISender sender, string id, [AsParameters] GetAccountQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Create a new Personal Info")]
    [EndpointDescription("Creates a new personal info using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreatePersonalInfo(ISender sender, int id, CreatePersonalInfoCommand command)
    {
        var piId = await sender.Send(command);

        return TypedResults.Created($"/account/{id}/personalinfo", id);
    }

    [EndpointSummary("Delete a Personal Info")]
    [EndpointDescription("Deletes the personal info with the specified ID.")]
    public static async Task<NoContent> DeletePersonalInfo(ISender sender, int id)
    {
        await sender.Send(new DeletePersonalInfoCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get Personal Info")]
    [EndpointDescription("Retrieves a personal info.")]
    public static async Task<Ok<PersonalInfoDto>> GetPersonalInfo(ISender sender, [AsParameters] GetPersonalInfoQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
