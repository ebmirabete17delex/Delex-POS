using Delex_POS.Application.Common.Security;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces;

namespace Delex_POS.Application.Users.Commands.AssignRole;

[Authorize(Roles = "Administrator")] // Only Admins can grant roles
public record AssignRoleCommand : IRequest<Result>
{
    public string UserId { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Result>
{
    private readonly IIdentityService _identityService;

    public AssignRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.AddToRoleAsync(request.UserId, request.Role);
    }
}