using Delex_POS.Application.Branches.Commands.CreateBranch;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
namespace Delex_POS.Application.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    private readonly IWarehouseQueryRepository _query;
    public CreateWarehouseCommandValidator(IWarehouseQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.WarehouseId)
            .NotEmpty()
            .NotNull()
            .MustAsync(WarehouseIdNotExist);

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull();
    }
    private async Task<bool> WarehouseIdNotExist(string warehouseId, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.WarehouseId == warehouseId, cancellationToken);
        return !exists;
    }
}
