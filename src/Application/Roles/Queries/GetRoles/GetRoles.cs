using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Domain.Entities.RBAC;

namespace Delex_POS.Application.Roles.Queries.GetRoles;

public record GetRolesQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<IdentityRoleDto>>
{};

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<IdentityRoleDto>>
{
    private readonly IIdentityService _identityService;

    public GetRolesQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<PaginatedList<IdentityRoleDto>> Handle(GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        var sortBy = "Name";
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var rolesQuery = _identityService.GetAllRoles(searchQuery, sortBy, sortDirection);

        return await rolesQuery
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
