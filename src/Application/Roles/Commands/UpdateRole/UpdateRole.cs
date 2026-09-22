using System.ComponentModel;
using Delex_POS.Application.Common.Interfaces.Repositories.Role;

namespace Delex_POS.Application.Roles.Commands.UpdateRole;

public record UpdateRoleCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;

    public UpdateRoleCommandHandler(IRoleCommandRepository roleCommandRepository, IRoleQueryRepository roleQueryRepository)
    {
        _roleCommandRepository = roleCommandRepository;
        _roleQueryRepository = roleQueryRepository;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _roleQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.Update(description: request.Description);
        await _roleCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}