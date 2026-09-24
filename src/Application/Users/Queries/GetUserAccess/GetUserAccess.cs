using System.Linq.Expressions;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Users.Queries.UserDTOs;

namespace Delex_POS.Application.Users.Queries.GetUserAccesses;

public record GetUserAccessQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<UserAccessDto>>
{
    public string UserId { get; init; } = string.Empty;
};

public class GetRoleAccessQueryHandler : IRequestHandler<GetUserAccessQuery, PaginatedList<UserAccessDto>>
{
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;
    private readonly IMapper _mapper;

    public GetRoleAccessQueryHandler(IMapper mapper, IUserAccessQueryRepository userAccessQueryRepository)
    {
        _userAccessQueryRepository = userAccessQueryRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserAccessDto>> Handle(GetUserAccessQuery request,CancellationToken cancellationToken)
    {   
        string? searchQuery = request.SearchQuery;

        Expression<Func<UserAccessDto, bool>> filter = searchQuery switch
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

        var accesses = _userAccessQueryRepository.GetByUserId(request.UserId, sortBy, sortDirection);

        return await accesses.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}