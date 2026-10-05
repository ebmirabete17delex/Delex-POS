using Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;

namespace Delex_POS.Application.ProductPurchasings.Commands.DeleteProductPurchasing;

public class DeleteProductPurchasingCommandValidator : AbstractValidator<DeleteProductPurchasingCommand>
{
    private readonly IProductPurchasingQueryRepository _query;
    public DeleteProductPurchasingCommandValidator(IProductPurchasingQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotNull().NotEmpty()
            .MustAsync(Exist);
    }
    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        var entity = await _query.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
