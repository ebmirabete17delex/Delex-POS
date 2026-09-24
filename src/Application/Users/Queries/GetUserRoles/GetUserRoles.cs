using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Users.Queries.UserDTOs;

namespace Delex_POS.Application.Users.Queries.GetUserRoles;

public record GetUserRolesQuery() : IRequest<List<UserRoleDto>>
{
    public string Id { get; init; } = string.Empty;
};

public class GetUserQueryHandler : IRequestHandler<GetUserRolesQuery, List<UserRoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetUserQueryHandler(IMapper mapper, IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<List<UserRoleDto>> Handle(GetUserRolesQuery request,CancellationToken cancellationToken)
    {
        var roles = await _identityService.GetUserRolesAsync(request.Id.Trim());
        return _mapper.Map<List<UserRoleDto>>(roles);
    }
}