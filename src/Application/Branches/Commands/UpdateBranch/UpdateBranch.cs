using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Branches.Commands.UpdateBranch;

public record UpdateBranchCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; } 
}

public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand>
{
    private readonly IBranchCommandRepository _branchCommandRepository;
    private readonly IBranchQueryRepository _branchQueryRepository;

    public UpdateBranchCommandHandler(IBranchCommandRepository branchCommandRepository, IBranchQueryRepository branchQueryRepository)
    {
        _branchCommandRepository = branchCommandRepository;
        _branchQueryRepository = branchQueryRepository;
    }

    public async Task Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.Update(
            request.Name, 
            request.Location, 
            request.Email,
            request.ContactNumber
            );
 
        await _branchCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}
