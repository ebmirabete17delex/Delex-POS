using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Branches.Queries.BranchDTOs;

namespace Delex_POS.Application.Branches.Queries.GetBranch;

public record GetBranchQuery() : IRequest<BranchDto>
{
    public int Id { get; init; }
};

public class GetBranchQueryHandler : IRequestHandler<GetBranchQuery, BranchDto>
{
    private readonly IMapper _mapper;
    private readonly IBranchQueryRepository _branchQueryRepository;

    public GetBranchQueryHandler(IMapper mapper, IBranchQueryRepository branchQueryRepository)
    {
        _mapper = mapper;
        _branchQueryRepository = branchQueryRepository;
    }

    public async Task<BranchDto> Handle(GetBranchQuery request,CancellationToken cancellationToken)
    {
        var branch = await _branchQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<BranchDto>(branch);
    }
}
