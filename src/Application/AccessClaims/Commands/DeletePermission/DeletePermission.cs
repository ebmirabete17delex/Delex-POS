using Delex_POS.Application.Common.Interfaces.Repositories.Permission;

namespace Delex_POS.Application.AccessClaims.Commands.DeletePermission;

public record DeletePermissionCommand : IRequest
{
    public int Id { get; set; } 
    public int AccessId { get; set; }
};
public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand>
{
    private readonly IPermissionCommandRepository _permissionCommandRepository;
    private readonly IPermissionQueryRepository _permissionQueryRepository;
    public DeletePermissionCommandHandler(
        IPermissionCommandRepository permissionCommandRepository, 
        IPermissionQueryRepository permissionQueryRepository)
    {
        _permissionQueryRepository = permissionQueryRepository;
        _permissionCommandRepository = permissionCommandRepository;
    }

    public async Task Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _permissionQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, permission);
        await _permissionCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
