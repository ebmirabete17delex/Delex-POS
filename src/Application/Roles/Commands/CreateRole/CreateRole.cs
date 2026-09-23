using Delex_POS.Application.Common.Interfaces;
namespace Delex_POS.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<(string Id, string Name)>
{
    public string Name { get; set; } = string.Empty;
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, (string Id, string Name)>
{
    private readonly IIdentityService _identityService;

    public CreateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<(string Id, string Name)> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.CreateRoleAsync(request.Name);

        return (result.RoleId, request.Name);
    }
}
