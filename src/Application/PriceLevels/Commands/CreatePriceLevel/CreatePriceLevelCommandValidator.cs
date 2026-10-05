using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;

namespace Delex_POS.Application.PriceLevels.Commands.CreatePriceLevel;

public class CreatePriceLevelCommandValidator : AbstractValidator<CreatePriceLevelCommand>
{
    private readonly IPriceLevelQueryRepository _query;
    public CreatePriceLevelCommandValidator(IPriceLevelQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MustAsync(NotExists);
    }
    private async Task<bool> NotExists(string name, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }

}
