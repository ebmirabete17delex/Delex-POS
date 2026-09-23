using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Application.Roles.Queries.GetRole;

public record GetRoleQuery() : IRequest<IdentityRoleDto>
{
    public string Id { get; init; } = string.Empty;
};

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IdentityRoleDto>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetRoleQueryHandler(IMapper mapper, IIdentityService identityService)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<IdentityRoleDto> Handle(GetRoleQuery request,CancellationToken cancellationToken)
    {   
        var role = await _identityService.GetRoleByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<IdentityRoleDto>(role);
    }
}
