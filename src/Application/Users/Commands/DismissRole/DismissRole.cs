using Delex_POS.Application.Common.Security;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces;

namespace Delex_POS.Application.Users.Commands.DismissRole;

[Authorize(Roles = "Administrator")] // Only Admins can grant roles
public record DismissRoleCommand : IRequest<Result>
{
    public string UserId { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}

public class DismissRoleCommandHandler : IRequestHandler<DismissRoleCommand, Result>
{
    private readonly IIdentityService _identityService;

    public DismissRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(DismissRoleCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.RemoveFromRoleAsync(request.UserId, request.Role);
    }
}