using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Branches.Commands.UpdateBranch;

public record UpdateBranchCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RegionId { get; set; }
    public int WarehouseId { get; set; }
    public string? Memo { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? TIN { get; set; }
    public string? Email { get; set; }
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
 
        await _branchCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}
