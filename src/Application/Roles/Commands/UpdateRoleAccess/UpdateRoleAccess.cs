using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;

namespace Delex_POS.Application.Roles.Commands.UpdateRoleAccess;

public record UpdateRoleAccessCommand : IRequest
{
    public string RoleId { get; set; } = string.Empty;
    public int AccessId { get; set; }
    public bool HasRead { get; set; }
    public bool HasWrite { get; set; }
    public bool HasUpdate { get; set; }
    public bool HasDelete { get; set; }
    public bool IsActive { get; set; }
}
public class UpdateRoleAccessCommandHandler : IRequestHandler<UpdateRoleAccessCommand>
{
    private readonly IRoleAccessCommandRepository _roleAccessCommandRepository;
    private readonly IRoleAccessQueryRepository _roleAccessQueryRepository;
    public UpdateRoleAccessCommandHandler(
        IRoleAccessQueryRepository roleAccessQueryRepository,
        IRoleAccessCommandRepository roleAccessCommandRepository)
    {
        _roleAccessCommandRepository = roleAccessCommandRepository;
        _roleAccessQueryRepository = roleAccessQueryRepository;
    }

    public async Task Handle(UpdateRoleAccessCommand request, CancellationToken cancellationToken)
    {
        var roleAccess = await _roleAccessQueryRepository
            .GetByRoleIdAndAccessIdAsync(request.RoleId, request.AccessId);
        Guard.Against.NotFound(request.AccessId, roleAccess);

        roleAccess.Update(
            request.HasRead,
            request.HasWrite,
            request.HasDelete,
            request.HasUpdate,
            request.IsActive);

        await _roleAccessCommandRepository.UpdateAsync(roleAccess, cancellationToken);
    }
}
