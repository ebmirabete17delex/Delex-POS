using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
namespace Delex_POS.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    public CreateUserCommandValidator(IBranchQueryRepository branchQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;
    
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(v => v.UserCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(v => v.LastName)
            .NotEmpty();

        RuleFor(v => v.FirstName)
            .NotEmpty();

        RuleFor(v => v.BranchId)
            .NotEmpty()
            .MustAsync(BranchIdExists);

    }
    private async Task<bool> BranchIdExists(int branchId, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == branchId, cancellationToken);
        return entity;
    }
}

