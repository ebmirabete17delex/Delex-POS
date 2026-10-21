using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.Permission;

namespace Delex_POS.Application.AccessClaims.Commands.CreatePermission;

public record CreatePermissionCommand : IRequest<int>
{
    public int AccessId { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class CreatePermissionCommandHandler : IRequestHandler <CreatePermissionCommand,int>
{
    private readonly IPermissionCommandRepository _permissionCommandRepository;
    private readonly IPermissionQueryRepository _permissionQueryRepository;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    public CreatePermissionCommandHandler(
        IPermissionCommandRepository permissionCommandRepository,
        IPermissionQueryRepository permissionQueryRepository,
        IAccessClaimQueryRepository accessClaimQueryRepository)
    {
        _permissionCommandRepository = permissionCommandRepository;
        _permissionQueryRepository = permissionQueryRepository;
        _accessClaimQueryRepository = accessClaimQueryRepository;
    }

    public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var access = await _accessClaimQueryRepository
            .GetByIdAsync(request.AccessId, cancellationToken);
        Guard.Against.NotFound(request.AccessId, access);
        var permission = await _permissionQueryRepository.GetAll()
            .SingleOrDefaultAsync(p => p.AccessId == request.AccessId && 
                                       p.Name == request.Name);
        if (permission is not null)
        {
            return permission.Id;
        }
        else
        {
            return await _permissionCommandRepository
                .AddAsync(new Domain.Entities.Permission(request.AccessId, 
                        request.Name), cancellationToken);
        }
    }
}
