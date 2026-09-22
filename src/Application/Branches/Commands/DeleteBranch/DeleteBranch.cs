using Delex_POS.Application.Common.Interfaces.Repositories.Branch ;

namespace Delex_POS.Application.Branches.Commands.DeleteBranch;

public record DeleteBranchCommand(int Id) : IRequest;

public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand>
{
    private readonly IBranchCommandRepository _branchCommandRepository;
    private readonly IBranchQueryRepository _branchQueryRepository;

    public DeleteBranchCommandHandler(IBranchCommandRepository branchCommandRepository, IBranchQueryRepository branchQueryRepository)
    {
        _branchCommandRepository = branchCommandRepository;
        _branchQueryRepository = branchQueryRepository;
    }

    public async Task Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _branchCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}