using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.Role;
using Delex_POS.Domain.Entities.RBAC;
using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Application.Roles.Queries.GetRoles;

public record GetRolesQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<RoleDto>>
{};

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IRoleQueryRepository _roleQueryRepository;

    public GetRolesQueryHandler(IMapper mapper, IRoleQueryRepository roleQueryRepository)
    {
        _mapper = mapper;
        _roleQueryRepository = roleQueryRepository;
    }

    public async Task<PaginatedList<RoleDto>> Handle(GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        Expression<Func<Role, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery)
                        || x.Description.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "AddedOn" : 
            request.SortBy.Equals("status", StringComparison.CurrentCultureIgnoreCase) ? "RoleStatus" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var RolesQuery = _roleQueryRepository.GetAll().Where(filter);

        IQueryable<RoleDto> projectedQuery = RolesQuery.ProjectTo<RoleDto>(_mapper.ConfigurationProvider);

        return await
            projectedQuery.OrderBy(sortBy, sortDirection)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
