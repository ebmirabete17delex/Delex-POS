using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
using Delex_POS.Application.Roles.Queries.RoleDTOs;

namespace Delex_POS.Application.Roles.Queries.GetRoleAccess;

public record GetRoleAccessQuery : IRequest<List<RoleAccessDto>>
{
    public string RoleId { get; init; } = string.Empty;
};

public class GetRoleAccessQueryHandler : IRequestHandler<GetRoleAccessQuery, List<RoleAccessDto>>
{
    private readonly IRoleAccessQueryRepository _roleAccessQueryRepository;

    public GetRoleAccessQueryHandler(IRoleAccessQueryRepository roleAccessQueryRepository)
    {
        _roleAccessQueryRepository = roleAccessQueryRepository;
    }

    public async Task<List<RoleAccessDto>> Handle(GetRoleAccessQuery request, CancellationToken cancellationToken)
    {   
        // Default to a property that exists on RoleAccessDto to avoid reflection lookup errors
        var accesses = _roleAccessQueryRepository.GetByRoleId(request.RoleId);
        Guard.Against.NotFound(request.RoleId, accesses);
        return await accesses.ToListAsync();
    }
}
