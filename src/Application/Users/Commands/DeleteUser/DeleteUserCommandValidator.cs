using Delex_POS.Application.Common.Interfaces.Repositories.User;
namespace Delex_POS.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly IUserQueryRepository _userQueryRepository;
    public DeleteUserCommandValidator(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    
        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(UserExists);
    }

    private async Task<bool> UserExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _userQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}


