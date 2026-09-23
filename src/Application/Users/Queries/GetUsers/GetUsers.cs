using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces;

namespace Delex_POS.Application.Users.Queries.GetUsers;

public record GetUsersQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<ApplicationUserDto>>
{};

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<ApplicationUserDto>>
{
    private readonly IIdentityService _identityService;
    public GetUsersQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<PaginatedList<ApplicationUserDto>> Handle(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "lastName" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var users = _identityService.GetAllUsers(searchQuery, sortBy, sortDirection);
            
        return await users
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
