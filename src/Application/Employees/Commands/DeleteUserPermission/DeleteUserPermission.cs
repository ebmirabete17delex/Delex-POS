using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;

namespace Delex_POS.Application.Employees.Commands.DeleteUserPermission;
public record DeleteUserPermissionCommand : IRequest
{
    public int UserId { get; set;  }
    public int Id { get; set; }
}
public class DeleteUserPermissionCommandHandler : IRequestHandler<DeleteUserPermissionCommand>
{
    private readonly IUserPermissionCommandRepository _userPermissionCommandRepository;
    private readonly IUserPermissionQueryRepository _userPermissionQueryRepository;
    public DeleteUserPermissionCommandHandler(
        IUserPermissionCommandRepository userPermissionCommandRepository,
        IUserPermissionQueryRepository userPermissionQueryRepository)
    {
        _userPermissionCommandRepository = userPermissionCommandRepository;
        _userPermissionQueryRepository = userPermissionQueryRepository;
    }
    public async Task Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userPermissionQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _userPermissionCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
