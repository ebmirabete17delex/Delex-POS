using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;

namespace Delex_POS.Application.Warehouses.Commands.UpdateWarehouse;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    private readonly IWarehouseQueryRepository _queryRepository;
    public UpdateWarehouseCommandValidator(IWarehouseQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;

        RuleFor(v => v.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(Exist);
    }
    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        var entity = await _queryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }

}
