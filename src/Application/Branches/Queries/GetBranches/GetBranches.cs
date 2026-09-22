using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Domain.Entities.RBAC;
using Delex_POS.Application.Branches.Queries.BranchDTOs;

namespace Delex_POS.Application.Branchs.Queries.GetBranches;

public record GetBranchesQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<BranchDto>>
{};

public class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, PaginatedList<BranchDto>>
{
    private readonly IMapper _mapper;
    private readonly IBranchQueryRepository _branchQueryRepository;

    public GetBranchesQueryHandler(IMapper mapper, IBranchQueryRepository branchQueryRepository)
    {
        _mapper = mapper;
        _branchQueryRepository = branchQueryRepository;
    }

    public async Task<PaginatedList<BranchDto>> Handle(GetBranchesQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        Expression<Func<Branch, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery)
                        || x.Location!.Contains(searchQuery)
                        || x.Email!.Contains(searchQuery)
                        || x.ContactNumber!.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "AddedOn" : 
            request.SortBy.Equals("status", StringComparison.CurrentCultureIgnoreCase) ? "BranchStatus" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var BranchsQuery = _branchQueryRepository.GetAll().Where(filter);

        IQueryable<BranchDto> projectedQuery = BranchsQuery.ProjectTo<BranchDto>(_mapper.ConfigurationProvider);

        return await
            projectedQuery.OrderBy(sortBy, sortDirection)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
