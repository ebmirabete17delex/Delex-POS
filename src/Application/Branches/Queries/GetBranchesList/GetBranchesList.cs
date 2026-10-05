using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Branches.Queries.BranchDTOs;

namespace Delex_POS.Application.Branches.Queries.GetBranchesList;

public record GetBranchesListQuery : 
    IRequest<List<BranchListDto>>;
public class GetBranchesListQueryHandler : 
    IRequestHandler<GetBranchesListQuery, List<BranchListDto>>
{
    private readonly IBranchQueryRepository _query;
    private readonly IMapper _mapper;
    public GetBranchesListQueryHandler(
        IMapper mapper, 
        IBranchQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<BranchListDto>> Handle(
        GetBranchesListQuery request, 
        CancellationToken cancellationToken)
    {
        var query = _query.GetAll().Where(b => b.IsActive == true);
        return await query.ProjectToListAsync<BranchListDto>(_mapper.ConfigurationProvider);
    }
}
