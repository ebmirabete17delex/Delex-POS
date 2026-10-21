using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Branches.Queries.BranchDTOs;

namespace Delex_POS.Application.Branches.Queries.GetBranch;

public record GetBranchQuery() : IRequest<BranchDto>
{
    public int Id { get; init; }
};

public class GetBranchQueryHandler : IRequestHandler<GetBranchQuery, BranchDto>
{
    private readonly IBranchQueryRepository _branchQueryRepository;

    public GetBranchQueryHandler(IBranchQueryRepository branchQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;
    }

    public async Task<BranchDto> Handle(GetBranchQuery request,CancellationToken cancellationToken)
    {
        var branch = await _branchQueryRepository
            .GetBranch(request.Id)
            .FirstAsync(e => e.Id == request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, branch);
        return branch;
    }
}
