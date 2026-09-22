using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.User;
using Delex_POS.Domain.Entities.RBAC;
using Delex_POS.Application.Users.Queries.UserDTOs;

namespace Delex_POS.Application.Users.Queries.GetUsers;

public record GetUsersQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<UserDto>>
{};

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserQueryRepository _userQueryRepository;

    public GetUsersQueryHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
    {
        _mapper = mapper;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<PaginatedList<UserDto>> Handle(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        Expression<Func<User, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.FirstName.Contains(searchQuery)
                        || x.LastName.Contains(searchQuery)
                        || x.Email.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "AddedOn" : 
            request.SortBy.Equals("status", StringComparison.CurrentCultureIgnoreCase) ? "UserStatus" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var usersQuery = _userQueryRepository.GetAll().Where(filter);

        IQueryable<UserDto> projectedQuery = usersQuery.ProjectTo<UserDto>(_mapper.ConfigurationProvider);

        return await
            projectedQuery.OrderBy(sortBy, sortDirection)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
