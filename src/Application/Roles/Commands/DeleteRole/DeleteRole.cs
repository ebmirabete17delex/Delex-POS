using Delex_POS.Application.Common.Interfaces.Repositories.Role ;

namespace Delex_POS.Application.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(int Id) : IRequest;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;

    public DeleteRoleCommandHandler(IRoleCommandRepository roleCommandRepository, IRoleQueryRepository roleQueryRepository)
    {
        _roleCommandRepository = roleCommandRepository;
        _roleQueryRepository = roleQueryRepository;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _roleQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _roleCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}