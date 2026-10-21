using Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;
using Delex_POS.Application.Common.Interfaces.Repositories.Permission;

namespace Delex_POS.Application.AccessClaims.Queries.GetAccessPermissions;

public record GetAccessPermissionsQuery(int Id) : IRequest<List<AccessPermissionDto>>;
public class GetAccessPermissionsQueryHandler : IRequestHandler<GetAccessPermissionsQuery,List<AccessPermissionDto>>
{
    private readonly IMapper _mapper;
    private readonly IPermissionQueryRepository _permissionQueryRepository;
    public GetAccessPermissionsQueryHandler(
        IMapper mapper,
        IPermissionQueryRepository permissionQueryRepository)
    {
        _mapper = mapper;
        _permissionQueryRepository = permissionQueryRepository;
    }
    public async Task<List<AccessPermissionDto>> Handle(GetAccessPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _permissionQueryRepository.GetAll()
            .Where(e => e.AccessId == request.Id)
            .ProjectTo<AccessPermissionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
