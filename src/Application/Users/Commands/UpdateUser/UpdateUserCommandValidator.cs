using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IBranchQueryRepository _branchQueryRepository;
    public UpdateUserCommandValidator(IIdentityService identityService, IBranchQueryRepository branchQueryRepository)
    {
        _identityService = identityService;
        _branchQueryRepository = branchQueryRepository;

        RuleFor(v => v.LastName)
            .NotEmpty()
            .MaximumLength(200);
        
        RuleFor(v => v.FirstName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.BranchId)
            .NotEmpty()
            .MustAsync(BranchIdExists);

        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(UserExists);
    }

    private async Task<bool> UserExists(string id, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetUserNameAsync(id);
        return entity != null;
    }

    private async Task<bool> BranchIdExists(int branchId, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == branchId, cancellationToken);
        return entity;
    }
}