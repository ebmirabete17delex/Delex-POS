using Delex_POS.Application.Common.Interfaces;
namespace Delex_POS.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly IIdentityService _identityService;
    public DeleteUserCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
    
        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(UserExists);
    }

    private async Task<bool> UserExists(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetUserNameAsync(id);
        return entity != null;
    }
}


