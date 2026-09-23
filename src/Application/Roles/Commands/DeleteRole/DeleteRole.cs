using Delex_POS.Application.Common.Interfaces;

namespace Delex_POS.Application.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(string Id) : IRequest;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IIdentityService _identityService;

    public DeleteRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetRoleByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _identityService.DeleteRoleAsync(request.Id);
    }
}