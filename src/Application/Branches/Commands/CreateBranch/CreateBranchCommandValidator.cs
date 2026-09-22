using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
namespace Delex_POS.Application.Branches.Commands.CreateBranch;

public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    public CreateBranchCommandValidator(IBranchQueryRepository branchQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;
    
        RuleFor(v => v.Code)
            .NotEmpty()
            .MustAsync(BranchCodeNotExists);

        RuleFor(v => v.Name)
            .NotEmpty()
            .MustAsync(BranchNameNotExists);
        
        RuleFor(v => v.Location)
            .NotEmpty();
        
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(v => v.ContactNumber)
            .NotEmpty();
    }

    private async Task<bool> BranchCodeNotExists(string code, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Code == code, cancellationToken);
        return !entity;
    }

    private async Task<bool> BranchNameNotExists(string name, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Name == name, cancellationToken);
        return !entity;
    }

}