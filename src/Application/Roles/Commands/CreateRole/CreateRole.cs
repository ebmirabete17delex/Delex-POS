using Delex_POS.Domain.Entities.RBAC;
using Delex_POS.Application.Common.Interfaces.Repositories.Role;

namespace Delex_POS.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IRoleCommandRepository _roleCommandRepository;

    public CreateRoleCommandHandler(IRoleCommandRepository roleCommandRepository)
    {
        _roleCommandRepository = roleCommandRepository;
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Role(
            name: request.Name,
            description: request.Description
        );

        return await _roleCommandRepository.AddAsync(entity,cancellationToken);
    }
}
