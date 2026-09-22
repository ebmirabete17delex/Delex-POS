using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
namespace Delex_POS.Application.Branches.Commands.UpdateBranch;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    public UpdateBranchCommandValidator(IBranchQueryRepository branchQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;

        RuleFor(v => v).MustAsync(BranchNameNotExists);
        
        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(BranchExists);

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);
        
        RuleFor(v => v.Location)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Email)
            .NotEmpty();

        RuleFor(v => v.ContactNumber)
            .NotEmpty();
    }

    private async Task<bool> BranchExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }

    private async Task<bool> BranchNameNotExists(UpdateBranchCommand cmd, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id != cmd.Id && e.Name == cmd.Name, cancellationToken);
        return !entity;
    }
}