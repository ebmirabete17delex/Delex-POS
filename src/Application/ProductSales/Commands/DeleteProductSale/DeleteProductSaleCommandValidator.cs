using Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;
namespace Delex_POS.Application.ProductSales.Commands.DeleteProductSale;

public class DeleteProductSaleCommandValidator : AbstractValidator<DeleteProductSaleCommand>
{
    private readonly IProductSaleQueryRepository _query;
    public DeleteProductSaleCommandValidator(IProductSaleQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(Exist);
    }
    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        var entity = await _query.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
