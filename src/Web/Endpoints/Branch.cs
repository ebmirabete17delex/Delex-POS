using Delex_POS.Application.Branches.Commands.CreateBranch;
using Delex_POS.Application.Branches.Commands.UpdateBranch;
using Delex_POS.Application.Branches.Commands.DeleteBranch;
using Delex_POS.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Delex_POS.Application.Branches.Queries.GetBranches;
using Delex_POS.Application.Branches.Queries.GetBranch;
using Delex_POS.Application.Branches.Queries.BranchDTOs;

namespace Delex_POS.Web.Endpoints;

public class Branch : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapPost(CreateBranch);
        groupBuilder.MapPut(UpdateBranch, "{id}");
        groupBuilder.MapDelete(DeleteBranch, "{id}");
        groupBuilder.MapGet(GetBranches);
        groupBuilder.MapGet(GetBranch, "{id}");
    }

    [EndpointSummary("Create a new Branch")]
    [EndpointDescription("Creates a new branch using the provided details and returns the ID of the created item.")]
    public static async Task<Created<int>> CreateBranch(ISender sender, CreateBranchCommand command)
    {
        int id = await sender.Send(command);

        return TypedResults.Created($"/Role/{id}", id);
    }

    [EndpointSummary("Update an Branch")]
    [EndpointDescription("Updates the specified branch. The ID in the URL must match the ID in the payload.")]
    public static async Task<Results<NoContent, BadRequest>> UpdateBranch(ISender sender, int id, UpdateBranchCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    [EndpointSummary("Delete a branch")]
    [EndpointDescription("Deletes the branch with the specified ID.")]
    public static async Task<NoContent> DeleteBranch(ISender sender, int id)
    {
        await sender.Send(new DeleteBranchCommand(id));

        return TypedResults.NoContent();
    }

    [EndpointSummary("Get all branches")]
    [EndpointDescription("Retrieves all branches.")]
    public static async Task<Ok<PaginatedList<BranchDto>>> GetBranches(ISender sender, [AsParameters] GetBranchesQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
    
    [EndpointSummary("Get branch")]
    [EndpointDescription("Retrieves a branch.")]
    public static async Task<Ok<BranchDto>> GetBranch(ISender sender, [AsParameters] GetBranchQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }
}
