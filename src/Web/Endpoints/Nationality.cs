using Delex_POS.Application.Nationalities.Queries.GetNationality;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Nationalities.Commands.CreateNationality;
using Delex_POS.Application.Nationalities.Commands.DeleteNationality;
using Delex_POS.Application.Nationalities.Commands.UpdateNationality;
using Delex_POS.Application.Nationalities.Queries.GetNationalities;
using Delex_POS.Application.Nationalities.Queries.GetNationalitiesList;
using Delex_POS.Application.Nationalities.Queries.NationalityDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class Nationality : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateNationality);
        groupBuilder.MapPut(UpdateNationality, "{id}");
        groupBuilder.MapDelete(DeleteNationality, "{id}");
        
        groupBuilder.MapGet(GetNationalities);
        groupBuilder.MapGet(GetNationalitiesList, "list");
        groupBuilder.MapGet(GetNationality, "{id}");
    }

    [EndpointSummary("Create a new Nationality")]
    [EndpointDescription("Creates a new nationality using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateNationality(ISender sender, CreateNationalityCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Update a Nationality")]
    [EndpointDescription("Updates the specified nationality. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateNationality(ISender sender, int id, UpdateNationalityCommand command)
    {
        command.Id = id;

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a Nationality")]
    [EndpointDescription("Deletes the nationality with the specified ID.")]
    public static async Task<NoContent> DeleteNationality(ISender sender, int id)
    {
        await sender.Send(new DeleteNationalityCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all Nationalities")]
    [EndpointDescription("Retrieves all nationalities.")]
    public static async Task<Ok<PaginatedList<NationalityDto>>> GetNationalities(ISender sender, [AsParameters] GetNationalitiesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Nationalities List")]
    [EndpointDescription("Retrieves all nationalities as a list.")]
    public static async Task<Ok<List<NationalityListDto>>> GetNationalitiesList(ISender sender, [AsParameters] GetNationalitiesListQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get Nationality")]
    [EndpointDescription("Retrieves a nationality.")]
    public static async Task<Ok<NationalityDto>> GetNationality(ISender sender, int id)
    {
        var vm = await sender.Send(new GetNationalityQuery(id));

        return TypedResults.Ok(vm);
    }
}
