using Delex_POS.Application.Common.Interfaces.Repositories.Role;
using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Application.Roles.Queries.GetRole;

public record GetRoleQuery() : IRequest<RoleDto>
{
    public int Id { get; init; }
};

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleDto>
{
    private readonly IMapper _mapper;
    private readonly IRoleQueryRepository _roleQueryRepository;

    public GetRoleQueryHandler(IMapper mapper, IRoleQueryRepository RoleQueryRepository)
    {
        _mapper = mapper;
        _roleQueryRepository = RoleQueryRepository;
    }

    public async Task<RoleDto> Handle(GetRoleQuery request,CancellationToken cancellationToken)
    {
        var Role = await _roleQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<RoleDto>(Role);
    }
}
