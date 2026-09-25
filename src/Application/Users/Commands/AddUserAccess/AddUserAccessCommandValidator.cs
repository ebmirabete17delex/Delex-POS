using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
namespace Delex_POS.Application.Users.Commands.AddUserAccess;

public class AddUserAccessCommandValidator : AbstractValidator<AddUserAccessCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;
    
    public AddUserAccessCommandValidator(
        IIdentityService identityService, 
        IAccessClaimQueryRepository accessClaimQueryRepository,
        IUserAccessQueryRepository userAccessQueryRepository)
    {
        _identityService = identityService;
        _accessClaimQueryRepository = accessClaimQueryRepository;
        _userAccessQueryRepository = userAccessQueryRepository;
    
        RuleFor(v => v)
            .MustAsync(UserAccessCombinationNotExist);

        RuleFor(v => v.UserId)
            .NotEmpty()
            .NotNull()
            .MustAsync(UserIdExist);

        RuleFor(v => v.AccessId)
            .NotEmpty()
            .NotNull()
            .MustAsync(AccessIdExist);
        
    }

    private async Task<bool> UserAccessCombinationNotExist(AddUserAccessCommand command, CancellationToken cancellationToken)
    {
        var exist = await _userAccessQueryRepository
        .ExistAsync(e => e.UserId == command.UserId && e.AccessId == command.AccessId, cancellationToken);
        return !exist;
    }

    private async Task<bool> UserIdExist(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetUserByIdAsync(id);
        return entity is not null;
    }

    private async Task<bool> AccessIdExist(int id, CancellationToken cancellationToken)
    {
        return await _accessClaimQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }
}