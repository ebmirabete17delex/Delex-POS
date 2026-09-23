using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Models;

namespace Delex_POS.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<string>
{
    public string Password { get; set; } = string.Empty;
    public int BranchId { get; set; }
    public string UserCode { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApplicationUserDto(
            branchId: request.BranchId,
            lastName: request.LastName,
            firstName: request.FirstName,
            middleName: request.MiddleName,
            email: request.Email,
            userCode: request.UserCode
        );

        (Result result, string identityId) = await _identityService.CreateUserAsync(entity, request.Password);

        return identityId;

    }
}
