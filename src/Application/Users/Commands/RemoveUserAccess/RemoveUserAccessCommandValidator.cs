using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
namespace Delex_POS.Application.Users.Commands.RemoveUserAccess;

public class RemoveUserAccessCommandValidator : AbstractValidator<RemoveUserAccessCommand>
{
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;
    
    public RemoveUserAccessCommandValidator(IUserAccessQueryRepository userAccessQueryRepository)
    {
        _userAccessQueryRepository = userAccessQueryRepository;
    
        RuleFor(v => v)
            .NotEmpty()
            .NotNull()
            .MustAsync(UserAccessCombinationExist);
    }

    private async Task<bool> UserAccessCombinationExist(RemoveUserAccessCommand command, CancellationToken cancellationToken)
    {
        var exist = await _userAccessQueryRepository
        .ExistAsync(e => e.UserId == command.UserId && e.AccessId == command.AccessId, cancellationToken);
        return exist;
    }
}