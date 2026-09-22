using Delex_POS.Application.Common.Interfaces.Repositories.User;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IBranchQueryRepository _branchQueryRepository;
    public UpdateUserCommandValidator(IUserQueryRepository userQueryRepository, IBranchQueryRepository branchQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
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

    private async Task<bool> UserExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _userQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }

    private async Task<bool> BranchIdExists(int branchId, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == branchId, cancellationToken);
        return entity;
    }
}