using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
namespace Delex_POS.Application.Branches.Commands.DeleteBranch;

public class DeleteBranchCommandValidator : AbstractValidator<DeleteBranchCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    public DeleteBranchCommandValidator(IBranchQueryRepository branchQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;
    
        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(BranchExists);
    }

    private async Task<bool> BranchExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}


