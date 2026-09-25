using Delex_POS.Domain.Entities.RBAC;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Branches.Commands.CreateBranch;

public record CreateBranchCommand : IRequest<int>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; } 
}

public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, int>
{
    private readonly IBranchCommandRepository _branchCommandRepository;

    public CreateBranchCommandHandler(IBranchCommandRepository branchCommandRepository)
    {
        _branchCommandRepository = branchCommandRepository;
    }

    public async Task<int> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = new Branch(
            code: request.Code,
            name: request.Name,
            location: request.Location,
            email: request.Email,
            contactNumber: request.ContactNumber
        );

        return await _branchCommandRepository.AddAsync(entity, cancellationToken);

    }
}
