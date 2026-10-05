using Delex_POS.Domain.Entities;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Branches.Commands.CreateBranch;

public record CreateBranchCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public int WarehouseId { get; set; }
    public int RegionId { get; set; }
    public string? Memo { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public string? TIN { get; set; }
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
            name: request.Name,
            warehouseId: request.WarehouseId,
            regionId: request.RegionId,
            memo: request.Memo,
            address1: request.Address1,
            address2: request.Address2,
            address3: request.Address3,
            phone: request.Phone,
            fax: request.Fax,
            email: request.Email,
            tin: request.TIN
        );

        return await _branchCommandRepository.AddAsync(entity, cancellationToken);

    }
}
