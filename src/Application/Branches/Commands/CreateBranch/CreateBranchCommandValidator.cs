using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Application.Common.Interfaces.Repositories.Region;
namespace Delex_POS.Application.Branches.Commands.CreateBranch;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateBranchCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    private readonly IWarehouseQueryRepository _warehouseQueryRepository;
    private readonly IRegionQueryRepository _regionQueryRepository;
    public CreateWarehouseCommandValidator(
        IBranchQueryRepository branchQueryRepository,
        IWarehouseQueryRepository warehouseQueryRepository,
        IRegionQueryRepository regionQueryRepository
        )
    {
        _branchQueryRepository = branchQueryRepository;
        _warehouseQueryRepository = warehouseQueryRepository;
        _regionQueryRepository = regionQueryRepository;
    
        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MustAsync(BranchNameNotExists);
        
        RuleFor(v => v.WarehouseId)
            .NotEmpty()
            .NotNull()
            .MustAsync(WarehouseIdExists);

        RuleFor(v => v.RegionId)
            .NotEmpty()
            .NotNull()
            .MustAsync(RegionIdExists);

        RuleFor(v => v.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();
        
    }

    private async Task<bool> BranchNameNotExists(string name, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Name == name, cancellationToken);
        return !entity;
    }
    private async Task<bool> WarehouseIdExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _warehouseQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
    private async Task<bool> RegionIdExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _regionQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
