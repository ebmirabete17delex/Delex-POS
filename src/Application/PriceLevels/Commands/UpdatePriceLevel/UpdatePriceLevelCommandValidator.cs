using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;

namespace Delex_POS.Application.PriceLevels.Commands.UpdatePriceLevel;

public class UpdatePriceLevelCommandValidator : AbstractValidator<UpdatePriceLevelCommand>
{
    private readonly IPriceLevelQueryRepository _query;
    public UpdatePriceLevelCommandValidator(IPriceLevelQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(Exists);
    }
    private async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await _query.ExistAsync(e => e.Id == id, cancellationToken);
    }
}
