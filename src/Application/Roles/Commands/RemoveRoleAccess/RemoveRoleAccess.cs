using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
namespace Delex_POS.Application.Roles.Commands.RemoveRoleAccess;

public record RemoveRoleAccessCommand : IRequest
{
    public string RoleId { get; set; } = string.Empty;
    public int AccessId { get; set; }
}

public class RemoveRoleAccessCommandHandler : IRequestHandler<RemoveRoleAccessCommand>
{
    private readonly IRoleAccessCommandRepository _roleAccessCommandRepository;
    private readonly IRoleAccessQueryRepository _roleAccessQueryRepository;

    public RemoveRoleAccessCommandHandler(
        IRoleAccessCommandRepository roleAccessCommandRepository,
        IRoleAccessQueryRepository roleAccessQueryRepository)
    {
        _roleAccessQueryRepository = roleAccessQueryRepository;
        _roleAccessCommandRepository = roleAccessCommandRepository;
    }

    public async Task Handle(RemoveRoleAccessCommand request, CancellationToken cancellationToken)
    {
        var entity = await _roleAccessQueryRepository
            .GetByRoleIdAndAccessIdAsync(request.RoleId, request.AccessId);
        
        Guard.Against.NotFound(request.AccessId, entity);

        await _roleAccessCommandRepository
            .DeleteAsync(entity.Id, cancellationToken);

    }
}
