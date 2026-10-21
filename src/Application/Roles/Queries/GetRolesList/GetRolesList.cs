using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Roles.Queries.RoleDTOs;
namespace Delex_POS.Application.Roles.Queries.GetRolesList;

public record GetRolesListQuery : IRequest<List<RoleDto>>;

public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, List<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    public GetRolesListQueryHandler(
        IMapper mapper,
        IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<List<RoleDto>> Handle(
        GetRolesListQuery request, 
        CancellationToken cancellationToken)
    {
        return await _identityService
            .GetAllRoles("","Name",Common.Enums.TableSort.ASC)
            .ProjectToListAsync<RoleDto>(_mapper.ConfigurationProvider);
    }

}
