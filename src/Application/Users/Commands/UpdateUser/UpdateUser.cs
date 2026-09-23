using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces;

namespace Delex_POS.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string UserCode {get; set;} = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public int BranchId { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUserDto(
            branchId: request.BranchId,
            lastName: request.LastName,
            firstName: request.FirstName,
            middleName: request.MiddleName,
            email: request.UserCode,
            userCode: request.UserCode
        );

        await _identityService.UpdateUserAsync(user: user);
    }
}
