using System.Linq.Expressions;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Application.Roles.Queries.GetRoleAccess;

public record GetRoleAccessQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<RoleAccessDto>>
{
    public string RoleId { get; init; } = string.Empty;
};

public class GetRoleAccessQueryHandler : IRequestHandler<GetRoleAccessQuery, PaginatedList<RoleAccessDto>>
{
    private readonly IRoleAccessQueryRepository _roleAccessQueryRepository;
    private readonly IMapper _mapper;

    public GetRoleAccessQueryHandler(IMapper mapper, IRoleAccessQueryRepository roleAccessQueryRepository)
    {
        _roleAccessQueryRepository = roleAccessQueryRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoleAccessDto>> Handle(GetRoleAccessQuery request,CancellationToken cancellationToken)
    {   
        string? searchQuery = request.SearchQuery;

        Expression<Func<RoleAccessDto, bool>> filter = searchQuery switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery)
                        || x.Name!.Contains(searchQuery)
                        || x.Feature!.Contains(searchQuery)
                        || x.BackendUrl!.Contains(searchQuery)
                        || x.FrontendUrl!.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var accesses = _roleAccessQueryRepository.GetByRoleId(request.RoleId, sortBy, sortDirection);

        return await accesses.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
