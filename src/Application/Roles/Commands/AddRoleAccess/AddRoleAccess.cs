using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
namespace Delex_POS.Application.Roles.Commands.AddRoleAccess;

public record AddRoleAccessCommand : IRequest<int>
{
    public string RoleId { get; set; } = string.Empty;
    public int AccessId { get; set; }
    public bool HasWrite { get; set; }
    public bool HasDelete { get; set; }
    public bool HasUpdate { get; set; }
}

public class AddRoleAccessCommandHandler : IRequestHandler<AddRoleAccessCommand, int>
{
    private readonly IRoleAccessCommandRepository _roleAccessCommandRepository;

    public AddRoleAccessCommandHandler(IRoleAccessCommandRepository roleAccessCommandRepository)
    {
        _roleAccessCommandRepository = roleAccessCommandRepository;
    }

    public async Task<int> Handle(AddRoleAccessCommand request, CancellationToken cancellationToken)
    {
        return await _roleAccessCommandRepository
            .AddAsync(new Domain.Entities.RoleAccess(
                request.RoleId, 
                request.AccessId,
                request.HasWrite,
                request.HasDelete,
                request.HasUpdate), cancellationToken);

    }
}
